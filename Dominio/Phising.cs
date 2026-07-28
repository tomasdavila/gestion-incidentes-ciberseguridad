using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Phising : Incidente
    {

        private string _canal;
        private bool _credenciales;
        private bool _transferenciaDatos;

        public string Canal { get { return _canal; } set { _canal = value; } }
        public bool Credenciales { get { return _credenciales; } set { _credenciales = value; } }
        public bool TransferenciaDatos { get { return _transferenciaDatos; } set { _transferenciaDatos = value; } }

        public Phising() { }

        public Phising(DateTime fechaRep, Activo activoAfectado, string descripcion, Estado estado,
                int impacto, int probabilidad,
                string canal, bool credenciales, bool transferenciaDatos)
     : base(fechaRep, activoAfectado, descripcion, estado, impacto, probabilidad)
        {
            _canal = canal;
            _credenciales = credenciales;
            _transferenciaDatos = transferenciaDatos;
        }
        public void ValidarPhising()
        {
            // Validamos que los campos no esten vacios
            if (string.IsNullOrEmpty(_canal)) throw new Exception("Los campos no pueden estar vacios");
        }

        public override string ToString()
        {
            return $"ID {Id} | Fecha: {FechaRep:dd/MM/yyyy} | Estado: {Estado} | Activo: {ActivoAfectado.CodA} | Impacto: {Impacto} | Prob: {Probabilidad} | Canal: {Canal} | Credenciales: {Credenciales} | Transferencia de Datos {TransferenciaDatos}";
        }

    }
}
