using Cadastro_Pessoa.Aplicacao;
using Cadastro_Pessoa.Dominio;
using Cadastro_Pessoa.ViewModel;
using System.Web.Mvc;

namespace Cadastro_Pessoa.Controllers
{
    public class PessoaController : Controller
    {
        [Route("Pessoa/Index")]
        public ActionResult Index()
        {
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
                return Index();
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
                return Index();
            }

            return AlterarPessoa(pessoa);
        }

        [HttpPost]
        public ActionResult Index(string textoPesquisa)
        {
            string campoPesquisa = Request.Form["TipoPesquisa"].ToString();
            var appPessoa = new PessoaAplicacao();
            var lista = appPessoa.ListarTodos(campoPesquisa, textoPesquisa);
            return View(lista);
        }
    }
}