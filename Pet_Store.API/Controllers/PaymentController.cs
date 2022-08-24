using Microsoft.AspNetCore.Mvc;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;
using Pet_Store.Infraestructure.Data;
using PetStore.Infraestructure.Repository;
using PetStore.Infraestructure.Repository.UnitOfWork;

namespace Pet_Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        readonly IRepository<Payments> _paymentsRepository;
        readonly IRepository<Users> _userRepository;

        public PaymentController(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _paymentsRepository = _unitOfWork.Repository<Payments>();
            _userRepository = _unitOfWork.Repository<Users>();
            
        }
        [HttpPost]
        [Route("payment")]
        public IActionResult CreatePayment([FromBody] newPayment model)
        {
            if (ModelState.IsValid)
            {
                var getUser = _userRepository.GetFirstOrDefault(x => x.Id == model.userId);

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
                        User = getUser
                    };
                    _paymentsRepository.Add(newPayment);
                    _unitOfWork.Save();
                    return Ok(model);
                }
                return BadRequest("usuario no existe");
            }
            return BadRequest("Error al crear pago");
        }
        [HttpGet]
        [Route("payment")]
        public IActionResult GetUserPayment(string id)
        {
            var payment = _paymentsRepository.GetFirstOrDefault(x => x.User.Id == id);

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
            var oldPayment = _paymentsRepository.GetFirstOrDefault(x => x.Id == model.Id);

            if (ModelState.IsValid)
            {
                var getUser = _userRepository.GetFirstOrDefault(x => x.Id == model.User.Id);
                
                oldPayment.firstName = model.firstName;
                oldPayment.lastName = model.lastName;
                oldPayment.zipCode = model.zipCode;
                oldPayment.cardNumber = model.cardNumber;
                oldPayment.CVV = model.CVV;
                oldPayment.expirationDate = model.expirationDate;
                oldPayment.User = getUser;

                _paymentsRepository.Update(model);
                _unitOfWork.Save();

                return Ok(model);
            }
            return BadRequest("Error al actualizar metodo de pago");
        }
        [HttpDelete]
        [Route("category")]
        public IActionResult Deletepayment(int id)
        {
            var payment = _paymentsRepository.GetFirstOrDefault(x => x.Id == id);

            if (payment != null)
            {
                _paymentsRepository.Remove(payment);
                _unitOfWork.Save();

                return Ok(200);
            }
            return BadRequest(400);
        }
    }
}