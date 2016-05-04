using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Text;
using Microsoft.BusinessData.Infrastructure.SecureStore;
using Microsoft.BusinessData.MetadataModel;
using Microsoft.BusinessData.Runtime;
using Microsoft.BusinessData.SystemSpecific;
using Microsoft.Office.SecureStoreService.Server;
using Microsoft.SharePoint;
using IContextProperty = Microsoft.BusinessData.SystemSpecific.IContextProperty;


namespace Modelos.Alumnos
{
    //Esta es la clase de implementacion del servicio hace todas las operaciones que queremos con nuestra base de datos
   
    public class AlumnoService:IContextProperty
    {
               
        public IExecutionContext ExecutionContext { get; set; }
        public ILobSystemInstance LobSystemInstance { get; set; }
        public IMethodInstance MethodInstance { get; set; }
        //
        

        public static void GetCredenciales(out string user, out string pwd)
        {
            //nos va a devolver el secure idapp (lo vamos a poner harcodeado y por eso es alumnos si no se pondria el nombre que ahi fuera)
            var appId = "Alumnos";
            user = "";
            pwd = "";

            //se encarga de darnos el acceso al securestorage
            ISecureStoreProvider provider=SecureStoreProviderFactory.Create();

            //container context
            ISecureStoreServiceContext providerContext=provider as ISecureStoreServiceContext;

            providerContext.Context = SPServiceContext.GetContext(new SPSite("http://pruebassp2"));

            //Recuperando las credenciales
            using (var creds=provider.GetCredentials(appId))
            {
                if (creds != null)
                {
                    foreach (var c in creds)
                    {
                        if (c.CredentialType == SecureStoreCredentialType.UserName)
                        {
                            user = GetCredentialFromString(c.Credential);
                        }
                        else if (c.CredentialType == SecureStoreCredentialType.Password)
                        {
                            pwd = GetCredentialFromString(c.Credential);
                        }
                    }
                }
            }
        }

        //Recuperar las credenciales y como estan cifradas y generadas ocn elemento que no es dll propia de c#
        //las librerias de marshal permiten hacer llamadas a ciertas librerias del sistema que no son de c# que no son de codigo manejado es decir a las tripas del sistema
        //Basicamente queremos convertir la credencial en texto y necesito tirar de una libreria externa
        private static string GetCredentialFromString(SecureString credential)
        {
            if (credential == null)
            {
                return null;
            }
            //inptr crea un puntero a un elemnto del sitema (cojo una direccion de memoria)
            IntPtr texto= IntPtr.Zero;
            try
            {
                texto = Marshal.SecureStringToBSTR(credential);
                //el puntero me lo devuelves en formato de texto
                return Marshal.PtrToStringBSTR(texto);
            }
            finally
            {
                if (texto != IntPtr.Zero)
                {
                    //si el puntero tiene algo, liberalo y limpialo que quede sin nada
                    Marshal.FreeBSTR(texto);
                }
            }
        }

        public static Alumno ReadItem(string id)
        {
            string pwd = "";
            string user = "";
            GetCredenciales(out user,out pwd);
            
            Alumno al = new Alumno();
            
            return al;
        }
        
        public static IEnumerable<Alumno> ReadList()
        {
            
            Alumno[] entityList = new Alumno[1];
            
            return entityList;
        }

        
    }

}
