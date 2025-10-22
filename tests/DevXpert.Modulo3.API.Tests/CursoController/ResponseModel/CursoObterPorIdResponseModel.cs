using DevXpert.Modulo3.API.Tests.Config;
using DevXpert.Modulo3.ModuloConteudo.Application.ViewModels;

namespace DevXpert.Modulo3.API.Tests.CursoController.ResponseModel
{
    public class CursoObterPorIdResponseModel : BaseResponseModel
    {
        public CursoViewModel Data { get; set; }
    }
}
