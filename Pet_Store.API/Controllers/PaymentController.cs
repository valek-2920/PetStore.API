using Microsoft.AspNetCore.Mvc;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;
using PetStore.DataAccess.Repository.UnityOfWork;
using System.IO.Compression;
using System.Reflection.Emit;

namespace Pet_Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUnityOfWork _unityOfWork;

        public PaymentController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpPost]
        [Route("payment")]
        public IActionResult CreatePayment([FromBody] newPayment model)
        {
            if (ModelState.IsValid)
            {

                var getUser = _unityOfWork.UsersRepository.GetFirstOrDefault(x => x.UserId == model.userId);

                if (getUser != null)
                {
                    Payments newPayment = new Payments
                    {
                        firstName = model.firstName,
                        lastName = model.lastName,
                        zipCode = model.zipCode,
                        cardNumber = model.zipCode,
                        CVV = model.CVV,
                        expirationDate = model.expirationDate,
                        user = getUser
                    };

                    _unityOfWork.PaymentsRepository.Add(newPayment);
                    _unityOfWork.Save();
                    return Ok(model);
                }

                return BadRequest("usuario no existe");
            }
            return BadRequest("Error al crear pago");
        }

        [HttpGet]
        [Route("payment")]
        public IActionResult GetUserPayment(int id)
        {
            var payment = _unityOfWork.PaymentsRepository.GetFirstOrDefault(x => x.user.UserId == id);
            _unityOfWork.Save();

            if (payment != null)
            {
                return Ok(payment);
            }
            return BadRequest("Usuario no posee metodo de pago");
        }

        [HttpPut]
        [Route("payment")]
        public IActionResult UpdatePaymentMethod([FromBody] Payments model)
        {
            var oldPayment = _unityOfWork.PaymentsRepository.GetFirstOrDefault(x => x.Id == model.Id);

            if (ModelState.IsValid)
            {
                var getUser = _unityOfWork.UsersRepository.GetFirstOrDefault(x => x.UserId == model.user.UserId);

                oldPayment.firstName = model.firstName;
                oldPayment.lastName = model.lastName;
                oldPayment.zipCode = model.zipCode;
                oldPayment.cardNumber = model.cardNumber;
                oldPayment.CVV = model.CVV;
                oldPayment.expirationDate = model.expirationDate;
                oldPayment.user = getUser;

                _unityOfWork.PaymentsRepository.Update(model);
                _unityOfWork.Save();

                return Ok(model);
            }
            return BadRequest("Error al actualizar metodo de pago");
        }

        [HttpDelete]
        [Route("category")]
        public IActionResult Deletepayment(int id)
        {
            var payment = _unityOfWork.PaymentsRepository.GetFirstOrDefault(x => x.Id == id);

            if (payment != null)
            {
                _unityOfWork.PaymentsRepository.Remove(payment);
                _unityOfWork.Save();

                return Ok(200);
            }
            return BadRequest(400);
        }
    }
}