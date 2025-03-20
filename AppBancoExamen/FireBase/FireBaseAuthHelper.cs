using Firebase.Auth;
using Firebase.Auth.Providers;

namespace AppBancoExamen.FireBase
{
    public class FireBaseAuthHelper
    {
        public const string firebaseAppId = "appbancoexa";
        public const string firebaseApikey = "AIzaSyAGDVuJGo6jiPbo1gVaMO6xcHKHo5_BWf8";

        public static FirebaseAuthClient setFirebaseAuthClient()
        {

            var auth = new FirebaseAuthClient(new FirebaseAuthConfig()
            {

                ApiKey = firebaseApikey,
                AuthDomain = $"{firebaseAppId}.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
             {
                 new EmailProvider()
             }


            });


            return auth;

        }


    }
}
