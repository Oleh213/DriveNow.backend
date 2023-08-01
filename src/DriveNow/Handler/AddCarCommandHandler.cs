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
using DriveNow.Model;
using DriveNow.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handlier
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
                    var categoty_main = await _context.catogories.FirstOrDefaultAsync(x => x.CategoryName == command._carModel.Category);

                    var fileExt = Path.GetExtension(command._carModel.FileUrl.FileName);

                    string docName = Guid.NewGuid().ToString() + fileExt;

                    Guid CarId = Guid.NewGuid();

                    UploadFile(command._carModel.FileUrl, docName);

                    if (categoty_main != null)
                    {
                        _context.cars.Add(new Car
                        {
                            CarId = CarId,
                            NameCar = command._carModel.NameCar,
                            Power = command._carModel.Power,
                            FromOneToHundred = command._carModel.FromOneToHundred,
                            MaxSpeed = command._carModel.MaxSpeed,
                            Passengers = command._carModel.Passengers,
                            Expenditure = command._carModel.Expenditure,
                            Price = command._carModel.Price,
                            Discount = 0,
                            Year = command._carModel.Year,
                            About = command._carModel.About,
                            AccualFileUrl = docName,
                            Address = command._carModel.Address,
                            CategoryForId = categoty_main.CategoryId
                        });

                        await _context.SaveChangesAsync();

                        return ("Successful!");
                    }
                    else if (categoty_main == null) {

                        Guid new_category_id = Guid.NewGuid();

                        _context.catogories.Add(new Catogory
                        {
                            CategoryName = command._carModel.Category,
                            CategoryId = new_category_id
                        }) ;

                        _context.cars.Add(new Car
                        {
                            CarId = CarId,
                            NameCar = command._carModel.NameCar,
                            Power = command._carModel.Power,
                            FromOneToHundred = command._carModel.FromOneToHundred,
                            MaxSpeed = command._carModel.MaxSpeed,
                            Passengers = command._carModel.Passengers,
                            Expenditure = command._carModel.Expenditure,
                            Price = command._carModel.Price,
                            Discount = 0,
                            Year = command._carModel.Year,
                            About = command._carModel.About,
                            AccualFileUrl = docName,
                            CategoryForId = new_category_id
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