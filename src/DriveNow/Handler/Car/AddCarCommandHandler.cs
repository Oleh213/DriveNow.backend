using System;
using System.Data;
using System.Security.Claims;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using AutoMapper;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Enums;
using DriveNow.Model;
using DriveNow.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler
{
    public class AddCarCommandHandler : IRequestHandler<CarCommand, string>
    {
        private ShopContext _context;

        private IStorageService _storageService;

        private IConfiguration _config;

        private IMapper _mapper;

        public AddCarCommandHandler(ShopContext ShopContext, IStorageService storageService,IConfiguration configuration, IMapper mapper)
        {
            _context = ShopContext;

            _storageService = storageService;

            _config = configuration;

            _mapper = mapper;
        }

       


        public string UserId = "38d5f673-5834-4c0b-bc21-5714a4a1fe27";

        public async Task<string> Handle(CarCommand command, CancellationToken cancellationToken)
        {
            string Url = "";

            var user_ckeck = await _context.users.FirstOrDefaultAsync(x => x.UserId == command._userId);

            if (user_ckeck != null)
            {
                var user = _mapper.Map<User, UserModel>(user_ckeck);

                if(user.Role == Enums.Role.Admin)
                {
                    var categoty_main = await _context.catogories.FirstOrDefaultAsync(x => x.CategoryName == command.NewCarInputModel.Category);

                    var fileExt = Path.GetExtension(command.NewCarInputModel.FileUrl.FileName);

                    string docName = Guid.NewGuid().ToString() + fileExt;

                    Guid CarId = Guid.NewGuid();

                    UploadFile(command.NewCarInputModel.FileUrl, docName);

                    if (categoty_main != null)
                    {
                        _context.cars.Add(new Car
                        {
                            CarId = CarId,
                            NameCar = command.NewCarInputModel.NameCar,
                            Power = command.NewCarInputModel.Power,
                            FromOneToHundred = command.NewCarInputModel.FromOneToHundred,
                            MaxSpeed = command.NewCarInputModel.MaxSpeed,
                            Passengers = command.NewCarInputModel.Passengers,
                            Expenditure = command.NewCarInputModel.Expenditure,
                            Price = command.NewCarInputModel.Price,
                            Discount = 0,
                            Year = command.NewCarInputModel.Year,
                            About = command.NewCarInputModel.About,
                            AccualFileUrl = docName,
                            Address = command.NewCarInputModel.Address,
                            Free = Free.Yes,
                            CategoryForId = categoty_main.CategoryId
                        });

                        await _context.SaveChangesAsync();

                        return ("Successful!");
                    }
                    else if (categoty_main == null) {

                        _context.cars.Add(new Car
                        {
                            CarId = CarId,
                            NameCar = command.NewCarInputModel.NameCar,
                            Power = command.NewCarInputModel.Power,
                            FromOneToHundred = command.NewCarInputModel.FromOneToHundred,
                            MaxSpeed = command.NewCarInputModel.MaxSpeed,
                            Passengers = command.NewCarInputModel.Passengers,
                            Expenditure = command.NewCarInputModel.Expenditure,
                            Price = command.NewCarInputModel.Price,
                            Discount = 0,
                            Year = command.NewCarInputModel.Year,
                            About = command.NewCarInputModel.About,
                            AccualFileUrl = docName,
                            Free = Free.Yes,
                            Catogories = new Catogory
                            {
                                CategoryName = command.NewCarInputModel.Category,
                                CategoryId = Guid.NewGuid()
                            },
                            Address = command.NewCarInputModel.Address,
                            PowerReserve = command.NewCarInputModel.PowerReserve
                        });

                        await _context.SaveChangesAsync();

                        return ("Successful!");
                    }
                }
                else
                {
                    return ("You are not an admin!");
                }
            }
            else
            {
                return ("You are not sign-up or registration");
            }

            return ("Bad");
        }
        public async Task<string> UploadFile(IFormFile file, string docName)
        {
            // Process file
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            //var docName = $"{Guid.NewGuid}";
            // call server

            var s3Obj = new S3Object()
            {
                BucketName = "drive-now",
                InputStream = memoryStream,
                Name = docName
            };

            var cred = new AwsCredentials()
            {
                AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                SecretKey = _config["AwsConfiguration:AWSSecretKey"]
            };

            var result = await _storageService.UploadFileAsync(s3Obj, cred);
            // 
            return (docName);

        }
    }
}