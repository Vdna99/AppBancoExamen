using AppBancoExamen.FireBase;
using Google.Cloud.Firestore;

namespace AppBancoExamen.Models
{
    public class UserModel
    {
       public string uuid { get; set; }

       public string Email { get; set; }
       public string Name { get; set; }
       

    }

    public class UserHelper {

        public async Task<UserModel> getUserInfo(string email) {

            Query query = FirestoreDb.Create(FireBaseAuthHelper.firebaseAppId).Collection("User").WhereEqualTo("Email", email);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            Dictionary<string, object> data = querySnapshot.Documents[0].ToDictionary();

            UserModel user = new UserModel { 
            
                Email = data["Email"].ToString(),
                Name = data["Name"].ToString()
            
            };

            return user;
        
        }
    
    }

}
