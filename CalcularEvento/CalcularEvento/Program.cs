using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcularEvento
{
    class Program
    {
        static void Main(string[] args)
        {
            string cInformacion = string.Empty, cMensaje=string.Empty;
            string[] lstInformacion = null;

            cInformacion=System.IO.File.ReadAllText(@"D:\BOT\Curso\Programa\CalcularEvento\texto.txt");
            lstInformacion = cInformacion.Split(',');

            cMensaje = ObtenerMensajes(lstInformacion);
            Console.Write(string.Format("Eventos: {0}", cMensaje));
            Console.ReadLine();
        }

        private static string ObtenerMensajes(string[] _lstInformacion)
        {
            string cMensaje = string.Empty, cEvento = string.Empty, cFecha = string.Empty;
            int iEventos = 0,iNumeroEventos = 0, iUbicacion=0;

            iNumeroEventos = ObtenerNumeroEventos(_lstInformacion);

            for (iEventos = 0; iEventos < iNumeroEventos; iEventos++)
            {
                cEvento = _lstInformacion[iUbicacion];
                iUbicacion++;
                cFecha = ObtenerTextoFecha(_lstInformacion[iUbicacion]);
                iUbicacion++;
                cMensaje += string.Format("{0} {1} ", cEvento, cFecha);
            }

            return cMensaje;
        }

        private static int ObtenerNumeroEventos(string[] _lstInformacion)
        {
            int iNumeroEventos = 0;
            iNumeroEventos= (_lstInformacion.Count())/2;
            return iNumeroEventos;
        }

        private static string ObtenerTextoFecha(string _cFecha)
        {
            string cTexto = string.Empty, cTextoInicial= string.Empty, cTiempoTranscurrido = string.Empty;
            DateTime dtFechaEvaluar = new DateTime();
            DateTime dtFecaActual = DateTime.Now;
            dtFechaEvaluar = DateTime.ParseExact(_cFecha,  "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cTextoInicial=ObtenerTextoInicial(dtFecaActual, dtFechaEvaluar);
            cTiempoTranscurrido = ObtenerTiempoTranscurrido(dtFecaActual, dtFechaEvaluar);
            cTexto = string.Format("{0} {1}", cTextoInicial, cTiempoTranscurrido);
            return cTexto;
        }

        private static string ObtenerTextoInicial(DateTime _dtFechaActual, DateTime _dtFechaEvaluar)
        {
            string cTexto = string.Empty;
            if (_dtFechaEvaluar < _dtFechaActual)
                cTexto = "ocurrió hace";
            else
                cTexto = "ocurrirá dentro de";
            return cTexto;
        }

        private static string ObtenerTiempoTranscurrido(DateTime _dtFechaActual, DateTime _dtFechaEvaluar)
        {
            string cTiempoTranscurrido = string.Empty;
            int iNumeroMinutos = 0, iNumeroHoras = 0, iNumeroDias = 0;
            TimeSpan tiempo = new TimeSpan();
            if (_dtFechaEvaluar < _dtFechaActual)
                tiempo = _dtFechaActual.Subtract(_dtFechaEvaluar);
            else
                tiempo = _dtFechaEvaluar.Subtract(_dtFechaActual);
            iNumeroMinutos =Convert.ToInt32(tiempo.TotalMinutes);
            if (iNumeroMinutos <= 59)
                cTiempoTranscurrido = string.Format("{0} Minutos", iNumeroMinutos);
            else
            {
                iNumeroHoras = Convert.ToInt32(tiempo.TotalHours);
                if(iNumeroHoras<=24)
                    cTiempoTranscurrido = string.Format("{0} Horas", iNumeroHoras);
                else
                {
                    iNumeroDias = Convert.ToInt32(tiempo.TotalDays);
                    if(iNumeroDias<30)
                        cTiempoTranscurrido = string.Format("{0} Días", iNumeroDias);
                    else
                    {
                        cTiempoTranscurrido = string.Format("{0} Meses", (iNumeroDias/30));
                    }
                }
            }
            return cTiempoTranscurrido;
        }
    }
}
