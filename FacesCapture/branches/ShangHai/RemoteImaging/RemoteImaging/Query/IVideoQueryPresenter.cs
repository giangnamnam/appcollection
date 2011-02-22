namespace RemoteImaging.Query
{
    public interface IVideoQueryPresenter
    {
        void Start();
        void Search();

        void PlayVideo();
        void ShowRelatedFaces();
        void ShowRelatedLicenseplates();

        void NextPage();
        void PreviousPage();
        void FirstPage();
        void LastPage();
    }
}