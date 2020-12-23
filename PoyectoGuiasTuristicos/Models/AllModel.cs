using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PoyectoGuiasTuristicos.Models
{
    public class AllModel
    {
        public IEnumerable<Usuario> UsuarioModel { get; set; }
        public IEnumerable<Ruta> RutaModel { get; set; }
        public IEnumerable<Museo> MuseoModel { get; set; }
        public IEnumerable<Valoracion> ValoracionModel { get; set; }
        public IEnumerable<ReservaOrganizaMuseo> ReservaMuseoModel { get; set; }
        public IEnumerable<ReservaOrganizaRuta> ReservaRutaModel { get; set; }
        public IEnumerable<Ciudad> CiudadModel { get; set; }

        public Valoracion valoracionModels { get; set; }
    }
}