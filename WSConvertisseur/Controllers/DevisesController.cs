using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSConvertisseur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevisesController : ControllerBase
    {
        private List<Devise> devises;

        public DevisesController()
        {
            devises = new List<Devise>();

            devises.Add(new Devise(1, "Dollar", 1.08));
            devises.Add(new Devise(2, "Franc Suisse", 1.07));
            devises.Add(new Devise(3, "Yen", 120));
        }

        /// <summary>
        /// Get all currency.
        /// </summary>
        /// <returns>List of Devise</returns>
        /// <response code="200">When the currency id is found</response>
        // GET: api/<DevisesController>
        [HttpGet]
        [ProducesResponseType(200)]
        public IEnumerable<Devise> GetAll()
        {
            return devises;
        }

        /// <summary>
        /// Get a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="200">When the currency id is found</response>
        /// <response code="404">When the currency id is not found</response>
        // GET api/<DevisesController>/5
        [HttpGet("{id}", Name = "GetDevise")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Devise> GetById(int id)
        {
            Devise? devise =
            (from d in devises
            where d.Id == id
            select d).FirstOrDefault();

            if (devise == null) 
            {
                return NotFound();
            }
            return devise;
        }

        /// <summary>
        /// Post a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="devise">The currency</param>
        /// <response code="201">When the currency is created</response>
        /// <response code="400">When the request is bad</response>
        // POST api/<DevisesController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            devises.Add(devise);
            return CreatedAtRoute("GetDevise", new {id =  devise.Id}, devise);
        }

        /// <summary>
        /// Put a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="201">When the currency is created</response>
        /// <response code="400">When the request is bad</response>
        // PUT api/<DevisesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Put(int id, [FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != devise.Id)
            {
                return BadRequest();
            }

            int index = devises.FindIndex((d) => d.Id == id);
            if (index < 0)
            {
                return NotFound();
            }
            devises[index] = devise;
            return NoContent();
        }

        /// <summary>
        /// Delete a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="200">When the currency id is found</response>
        /// <response code="404">When the currency id is not found</response>
        // DELETE api/<DevisesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Devise> Delete(int id)
        {
            Devise? devise =
            (from d in devises
             where d.Id == id
             select d).FirstOrDefault();

            if (devise == null)
            {
                return NotFound();
            }
            devises.Remove(devise);
            return devise;
        }
    }
}
