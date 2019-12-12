using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Untappd;
using Xamarin.Forms;
using XamarinBeer.CustomVision;
using XamarinBeer.Speech;

namespace XamarinBeer
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private byte[] _imageInBytes;
        private MemoryStream GetImageAsStream() => new MemoryStream(_imageInBytes);
        public event PropertyChangedEventHandler PropertyChanged;

        public SpeechService _speechService;
        public PredictionClient _predictionClient;
        public TrainingClient _trainingClient;
        public UntappdClient _untappdClient;

        public MainPageViewModel()
        {
            if(DesignMode.IsDesignModeEnabled)
            {
                return;
            }
            _predictionClient = new PredictionClient();
            _trainingClient = new TrainingClient();
            _untappdClient = new UntappdClient(new HttpClient(), Config.UntappdClientId, Config.UntappdSecret);
            _speechService = new SpeechService();

            CrossMedia.Current.Initialize().Wait();
        }

        public ICommand PredictBeerCommand => new Command(async () => {
            try
            {
                await PredictBeer();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        });

        public ICommand AddBeerImageCommand => new Command(async () => {
            try
            {
                await AddBeerImage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        });

        public ICommand TrainTheNetworkCommand => new Command(async () => {
            try
            {
                await TrainTheNetwork();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        });

        private ImageSource _imageSource;
        public ImageSource CurrentImageSource {
            get => _imageSource;
            set {
                _imageSource = value;
                RaisePropertyChanged(nameof(CurrentImageSource));
            }
        }

        private string _description = "";
        public string Description { 
            get => _description; 
            set {
                _description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        private string _beerId = "";
        public string BeerId {
            get => _beerId;
            set {
                _beerId = value;
                RaisePropertyChanged(nameof(BeerId));
            }
        }

        public async Task PredictBeer()
        {
            Description = "Recognizing beer...";
            await TakeAPicture();
            SetBackground();
            await PredictThisBeerImage();
            await SayItLoud();
        }

        private async Task AddBeerImage()
        {
            Description = "Adding new image...";
            await TakeAPicture();
            SetBackground();
            await AddThisBeerImage();
            await SayItLoud();
        }

        public async Task TakeAPicture()
        {
            _imageInBytes = null;
            CurrentImageSource = null;
            
            var file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    SaveToAlbum = false,
                    PhotoSize = PhotoSize.Medium,
                    DefaultCamera = CameraDevice.Rear,
                    CompressionQuality = 92
                }
            );
            if (file == null)
            {
                return;
            }
            var mem = new MemoryStream();
            await file.GetStream().CopyToAsync(mem);
            file.Dispose();
            _imageInBytes = mem.ToArray();
        }

        private void SetBackground()
        {
            CurrentImageSource = ImageSource.FromStream(() => GetImageAsStream());
        }

        private async Task PredictThisBeerImage()
        {
            var model = await _trainingClient.GetLatestModel();
            var beerId = await _predictionClient.Predict(new MemoryStream(_imageInBytes), model);
            var beer = await _untappdClient.GetBeer(beerId);
            
            BeerId = beerId.ToString();
            Description = beer.GetCustomDescription();
        }

        private async Task AddThisBeerImage()
        {
            if(String.IsNullOrWhiteSpace(BeerId))
            {
                Description = "Inform the beer id!";
                return;
            }
            if(_imageInBytes == null)
            {
                return;
            }
            var success = await _trainingClient.AddImage(new MemoryStream(_imageInBytes), BeerId);
            if(!success)
            {
                Description = "Could not add image to model";
                return;
            }
            var beerId = Convert.ToInt32(BeerId);
            var beer = await _untappdClient.GetBeer(beerId);
            Description = $"Beer {beer.BeerName} added successfully!";
        }


        private async Task SayItLoud()
        {
            await _speechService.SpeakTextAsync(Description);
        }

        public async Task TrainTheNetwork()
        {
            if (!await Confirm())
            {
                return;
            }            
            Description = "Training the network...";
            var iterationId = await _trainingClient.TrainModel();
            Description = "Publishing the iteration...";
            await _trainingClient.PublishIteration(iterationId, iterationId.ToString());
            Description = "Training Done!";
        }


        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private async Task<bool> Confirm()
        {
            return await Application.Current.MainPage.DisplayAlert("", "Are you sure?", "OK", "Cancel");
        }

        public ICommand PredictBeerCommand2 => new Command(async () => {            
            Description = "Recognizing beer...";
            await TakeAPicture();
            if(_imageInBytes == null)
            {
                Description = "";
                return;
            }
            SetBackground();
            Description = "Warning! This is a Heineken. This beer is not allowed in this country. Security was notified!";
            await SayItLoud();
        });

    }
}
