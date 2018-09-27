using Android.App;
using Android.OS;
using Android.Widget;
using FrontMobileApithon.Services;
using eBooks.Services;
using System.Threading.Tasks;
using Android.Content;

namespace FrontMobileApithon.Droid.Implementations.Splash
{
    [Activity(Label = "SplashActivity")]
    public class SplashActivity : Activity
    {
        TextView appVersionTextView;
        TextView loadingTextView;
        private ApiConsumer ApiService;
        private CheckConnection CheckConnection;
        private LinearLayout contentMessageLayout;
        private ImageView statusImageView;
        private TextView messagetextView;

        public SplashActivity()
        {
            ApiService = new ApiConsumer();
            CheckConnection = new CheckConnection();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Splash);
            InitControls();

        }

        protected override void OnStart()
        {
            base.OnStart();
            // Load the main screen activity
            LoadConfiguration();
        }

        private async void LoadConfiguration()
        {
            var Connection = await CheckConnection.Check();
            if (!Connection.IsSuccess)
            {
                CreateStatusTurnNoternetConnection(Connection.Message);
                return;
            }

            await Task.Delay(1500);

            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }

        private void CreateStatusTurnNoternetConnection(string message)
        {
            
        }

        private void InitControls()
        {
            // Get the references to the view elements
            appVersionTextView = FindViewById<TextView>(Resource.Id.appVersionTextView);
            loadingTextView = FindViewById<TextView>(Resource.Id.loadingTextView);
        }
    }
}