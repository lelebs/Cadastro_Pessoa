using Cadastro_Pessoa.Aplicacao;
using Cadastro_Pessoa.Dominio;
using System.Web.Mvc;

namespace Cadastro_Pessoa.Controllers
{
    public class PessoaController : Controller
    {
        [Route("Pessoa/Index/{textoPesquisa?}")]
        public ActionResult Index(string textoPesquisa)
        {
            if(textoPesquisa != null)
            {
                var campoPesquisa = Request.Form["TipoPesquisa"].ToString();
                var appPessoa = new PessoaAplicacao();
                var lista = appPessoa.ListarTodos(campoPesquisa, textoPesquisa);

                return PartialView("_Pessoas", lista);
            }

            return View();
        }

        // GET: Pessoa
        [Route("Pessoa/Inserir")]
        public ActionResult Inserir()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Inserir(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                var appPessoa = new PessoaAplicacao();
                appPessoa.InserirPessoa(pessoa);
                string id = appPessoa.ListarUltimoInserido();
                return Index(id);
            }

            return Inserir(pessoa);
        }

        [HttpPost]
        public ActionResult AlterarPessoa(Pessoa pessoa)
        {
            if(ModelState.IsValid)
            {
                var appPessoa = new PessoaAplicacao();
                appPessoa.AlterarPessoa(pessoa);
                return Index(null);
            }

            return AlterarPessoa(pessoa);
        }
    }
}