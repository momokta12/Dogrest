using Microsoft.AspNetCore.Mvc;
using DogLib;
using System.Diagnostics.Eventing.Reader;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dogrest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogController : ControllerBase
    {
        private DogsRepository _dogsRepository; // her gør vi brug af vores repository
        public DogController(DogsRepository dogsRepository) // her injecter vi vores repository i vores controller
        {
            _dogsRepository = dogsRepository;
        }



        // GET: api/<DogController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<Dog>> Get()
        {
            IEnumerable<Dog> dogs = _dogsRepository.GetAll(); // her henter vi alle hunde fra vores repository
            // IEnumerable betyder egentlig bare at det er en liste af noget
            if (dogs.Count() == 0) 
            {
                return NoContent(); // hvis der ikke er nogen hunde i vores repository, returnerer vi en 204
            }
            else
            {
                return Ok(dogs); // ellers returnerer vi en 200 med listen af hunde

            }

        }

        // GET api/<DogController>/5
        [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Dog> Get(int id)
        {
            Dog? dog = _dogsRepository.GetById(id); // her henter vi en hund fra vores repository
            if 
                (dog is null)
            {
                return NotFound(); // hvis hunden ikke findes, returnerer vi en 404
            }
            else
            {
                return Ok(dog); // ellers returnerer vi en 200 med hunden
            }
        }

        // POST api/<DogController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Dog> Post([FromBody] Dog dog)
        {
            try
            {
                Dog newDog = _dogsRepository.Add(dog); // her tilføjer vi en hund til vores repository
                return CreatedAtAction(nameof(Get), new { id = newDog.Id }, newDog); // vi returnerer en 201 med hunden
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message); // hvis der sker en fejl, returnerer vi en 400 med fejlbeskeden
            }

        }

        // DELETE api/<DogController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Dog> Delete(int id)
        {
            Dog? dog = _dogsRepository.GetById(id); // her
            if (dog is null)
            {
                return NotFound(); // hvis hunden ikke findes, returnerer vi en 404
            }
            else
            {
                _dogsRepository.Delete(id); // ellers sletter vi hunden fra vores repository
                return Ok(dog); // og returnerer en 200 med hunden
            }
        }

        [HttpGet]
        [Route("sortbyage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<Dog>> SortByAge()
        {
            IEnumerable<Dog> dogs = _dogsRepository.SortByAge(); // her henter vi alle hunde fra vores repository
            // IEnumerable betyder egentlig bare at det er en liste af noget
            if (dogs.Count() == 0)
            {
                return NoContent(); // hvis der ikke er nogen hunde i vores repository, returnerer vi en 204
            }
            else
            {
                return Ok(dogs); // ellers returnerer vi en 200 med listen af hunde

            }

        }
        
        [HttpGet]
        [Route("sortbyname")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<Dog>> SortByName()
        {
            IEnumerable<Dog> dogs = _dogsRepository.SortByName(); // her henter vi alle hunde fra vores repository
            // IEnumerable betyder egentlig bare at det er en liste af noget
            if (dogs.Count() == 0)
            {
                return NoContent(); // hvis der ikke er nogen hunde i vores repository, returnerer vi en 204
            }
            else
            {
                return Ok(dogs); // ellers returnerer vi en 200 med listen af hunde

            }

        }

        [HttpGet]
        [Route("filterbyname")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<Dog>> FilterByName([FromQuery] string name)
        {
            IEnumerable<Dog> dogs = _dogsRepository.FilterByName(name); // her henter vi alle hunde fra vores repository
            // IEnumerable betyder egentlig bare at det er en liste af noget
            if (dogs.Count() == 0)
            {
                return NoContent(); // hvis der ikke er nogen hunde i vores repository, returnerer vi en 204
            }
            else
            {
                return Ok(dogs); // ellers returnerer vi en 200 med listen af hunde

            }


        }
    }
}
