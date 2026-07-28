using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Ramsonware : Incidente
    {
        private bool _datosEncriptados;
        private bool _exfiltracion;

        public bool DatosEncriptados { get { return _datosEncriptados; } set { _datosEncriptados = value; } }
        public bool Exfiltracion { get { return _exfiltracion; } set { _exfiltracion = value; } }

        public Ramsonware() { }

        public Ramsonware(DateTime fechaRep, Activo activoAfectado, string descripcion, Estado estado,
                      int impacto, int probabilidad,
                      bool datosEncriptados, bool exfiltracion)
        : base(fechaRep, activoAfectado, descripcion, estado, impacto, probabilidad)
        {
            _datosEncriptados = datosEncriptados;
            _exfiltracion = exfiltracion;
        }

        public override string ToString()
        {
            return $"ID {Id} | Fecha: {FechaRep:dd/MM/yyyy} | Estado: {Estado} | Activo: {ActivoAfectado.CodA} | Impacto: {Impacto} | Prob: {Probabilidad} | Datos Encriptados: {DatosEncriptados} | Exfiltracion: {Exfiltracion}  ";
        }

        public override int CalculoSeveridad()
        {
            int severidad = base.CalculoSeveridad();

            if (_datosEncriptados == true)
            { severidad += 20; }

            if (_exfiltracion == true)
            {
                severidad += 25;
            }

            if (severidad >= 100)
            {
                severidad = 100;
            }

            return severidad;
        }

    }
}
