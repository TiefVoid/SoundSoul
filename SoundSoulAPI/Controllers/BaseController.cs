using Microsoft.AspNetCore.Mvc;

namespace SoundSoulAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        private HttpRequest? Request { get; set; }
        private IConfiguration? Configuration { get; set; }

        /**
        * Constructor
        * <param name="configuration">Requiere un tipo de IConfiguration para leer propiedades de appsettings.json</param>
        */
        protected BaseController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /**
        * Permite obtener un valor desde appsetting
        * Warnning: Requiere estar inicializado Configuration desde el constructor
        * <param name="key">Nombre de la propiedad</param>
        */
        protected T GetValueConfiguration<T>(string key)
        {
            return Configuration!.GetValue<T>(key);
        }

        /**
     * Permite realizar una respuesta con codigo 400, si falla alguna validacion en el controlador
     */
        protected ObjectResult GetBadResponseByValidator()
        {
            return Problem(ModelState.ToString()!);
        }

        /**
         * Permite realizar una resṕuesta 
         */
        protected IActionResult GetResponse(ResponseApi response)
        {
            if (response.Result)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}