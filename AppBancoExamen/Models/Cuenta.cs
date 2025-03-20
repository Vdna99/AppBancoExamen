using AppBancoExamen.FireBase;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace AppBancoExamen.Models
{
    public class Cuenta
    {
        public string id { get; set; }

        public string EmailC { get; set; }

        public int NCuenta { get; set; }

        public int Saldo { get; set; }
    }


    public static class CuentasHelper 
    {
        public static async Task<List<Cuenta>> GetCuentas(string email)
        {
            List<Cuenta> Cuentalist = new List<Cuenta>();

            Query query = FirestoreDb.Create(FireBaseAuthHelper.firebaseAppId).Collection("Cuentas").WhereEqualTo("Email",email);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            foreach (var item in querySnapshot) 
            {
                Dictionary<string, object> data = item.ToDictionary();

                Cuentalist.Add(new Cuenta
                {
                    id = item.Id,
                    EmailC = data["Email"].ToString(),
                    NCuenta = Convert.ToInt32( data["NCuenta"]),
                    Saldo = Convert.ToInt32 ( data["Saldo"])


                }); 

                
            
            }
            return Cuentalist;

        }

        public static async Task<Cuenta?> GetCuentaInfo(string email) {



            Query query = FirestoreDb.Create(FireBaseAuthHelper.firebaseAppId).Collection("Cuentas").WhereEqualTo("Email", email);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            Dictionary<string, object> data = querySnapshot.Documents[0].ToDictionary();

            Cuenta infocuenta = new Cuenta
            {
                id = data["id"].ToString(),
                EmailC = data["Email"].ToString(),
                NCuenta = Convert.ToInt32(data["NCuenta"]),
                Saldo = Convert.ToInt32(data["Saldo"])
            };

            return infocuenta;
        }

        public static async Task<Cuenta?> GetCuentaInfo2(string ncuenta)
        {



            Query query = FirestoreDb.Create(FireBaseAuthHelper.firebaseAppId).Collection("Cuentas").WhereEqualTo("NCuenta", ncuenta);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            Dictionary<string, object> data = querySnapshot.Documents[0].ToDictionary();

            Cuenta infocuenta = new Cuenta
            {
                id = data["id"].ToString(),
                NCuenta = Convert.ToInt32(data["NCuenta"]),
                Saldo = Convert.ToInt32(data["Saldo"])
            };

            return infocuenta;
        }

       

    }

}


