using AppBancoExamen.FireBase;
using Firebase.Auth;
using Google.Cloud.Firestore;
using Google.Type;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AppBancoExamen.Models
{
    public class TransaccionesModel
    {
        public int Id { get; set; }

        public int NumeroCuenta { get; set; }

        public string Motivo { get; set; }

        public int Cantidad { get; set; }

        public string FechaIni { get; set; }

        public string Email { get; set; }



    }

      public class TransaccionHelper 
      {

        public static async Task<List<TransaccionesModel>> GetTransacciones(string email)
        {


            List<TransaccionesModel> transaccionesList = new List<TransaccionesModel>();


            Query query = FirestoreDb.Create(FireBaseAuthHelper.firebaseAppId).Collection("Transacciones").WhereEqualTo("Email", email);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            foreach (var item in querySnapshot)
            {
                Dictionary<string, object> data = item.ToDictionary();

                transaccionesList.Add(new TransaccionesModel
                {
                    Email = data["Email"].ToString(),
                    NumeroCuenta = Convert.ToInt32(data["NumeroCuenta"]),
                    Motivo = data["Motivo"].ToString(),
                    Cantidad = Convert.ToInt32(data["Cantidad"]),
                    FechaIni = data["Fecha"].ToString(),
                   

                });



            }
            return transaccionesList;


        }
        public static async Task<bool> UpdateCuentasTrac(TransaccionesModel transacciones) 
        {
            try
            {
                Cuenta cuenta = await CuentasHelper.GetCuentaInfo2(transacciones.NumeroCuenta.ToString());

                Cuenta cuenta1 = await CuentasHelper.GetCuentaInfo(transacciones.Email);

                int suma = cuenta.Saldo + transacciones.Cantidad;

                string valor = "zvmlbRBqAb3xP9WqzTr9Z";
                DocumentReference docRef = FirestoreDb.Create(FireBaseAuthHelper.firebaseAppId).Collection("Cuentas").Document(cuenta.id);

                Dictionary<string, object> dataToUpdate = new Dictionary<string, object>
                {
                    {"Saldo",suma },



                };
                WriteResult result = await docRef.UpdateAsync(dataToUpdate);

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public static async Task<bool> UpdateCuentasMenos(TransaccionesModel transacciones)
        {
            try
            {
                Cuenta cuenta = await CuentasHelper.GetCuentaInfo2(transacciones.NumeroCuenta.ToString());

                Cuenta cuenta1 = await CuentasHelper.GetCuentaInfo(transacciones.Email);

                int Resta = cuenta1.Saldo - transacciones.Cantidad;

                string valor = "zvmlbRBqAb3xP9WqzTr9Z";
                DocumentReference docRef = FirestoreDb.Create(FireBaseAuthHelper.firebaseAppId).Collection("Cuentas").Document(cuenta1.id);

                Dictionary<string, object> dataToUpdate = new Dictionary<string, object>
                {
                    {"Saldo",Resta },



                };
                WriteResult result = await docRef.UpdateAsync(dataToUpdate);

                return true;
            }
            catch
            {
                return false;
            }
        }


        public static async Task<bool> saveTransaccion(TransaccionesModel transacciones)
        {
            try
            {
                //Optener cuentas
                Cuenta cuenta = await CuentasHelper.GetCuentaInfo2(transacciones.NumeroCuenta.ToString());

                if (cuenta != null)
                {


                }
                else {
                    return false;
                }

             
                 Cuenta cuenta1 = await CuentasHelper.GetCuentaInfo(transacciones.Email);

                if (cuenta1.Saldo >= transacciones.Cantidad)
                {

                   
                }
                else {
                    return false;
                }

               UpdateCuentasTrac(transacciones);
                UpdateCuentasMenos(transacciones);
                

                //Save
                DocumentReference docRef1 = await FirestoreDb.Create(FireBaseAuthHelper.firebaseAppId).Collection("Transacciones").AddAsync(
                    new Dictionary<string, object>
                    {

                        {"Cantidad",transacciones.Cantidad},
                        {"Email",transacciones.Email},
                        {"Fecha",transacciones.FechaIni},
                        {"Motivo",transacciones.Motivo},
                        {"NumeroCuenta",transacciones.NumeroCuenta},

                    });

                return true;
            }      
            
            catch 
            {
                return false;
            }
        
        
        
        
        }




      }



}
