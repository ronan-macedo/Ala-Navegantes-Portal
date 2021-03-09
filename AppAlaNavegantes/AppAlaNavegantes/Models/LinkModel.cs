using AppAlaNavegantes.Services;
using Microsoft.Extensions.Options;

namespace AppAlaNavegantes.Models
{
    public class LinkModel
    {
        public string Sacramental { get; set; }
        public string QuorumElderes { get; set; }
        public string SocidadeSocorro { get; set; }
        public string SacerdocioAaronico { get; set; }
        public string Mocas { get; set; }
        public string Primaria { get; set; }
        public string EscolaDominical { get; set; }
        public string EscolaDominicalJovens { get; set; }
        public string FrequenciaEscDomAdultos { get; set; }
        public string FrequenciaEscDomJovens { get; set; }
    }
}
