using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands;

public class ShowCarDetailsCommand: IRequest<CarModel>
{
    public Guid Car_Id { get; set; }

    public ShowCarDetailsCommand(Guid carId)
    {
        Car_Id = carId;
    }
}