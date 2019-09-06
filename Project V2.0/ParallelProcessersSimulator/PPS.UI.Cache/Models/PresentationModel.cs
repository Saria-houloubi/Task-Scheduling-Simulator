using PPS.UI.Shared.Models.Base;
namespace PPS.UI.Cache.Models
{
    public class PresentationModel : BaseNotifyModel
    {
        #region properties
        /// <summary>
        /// holds the number of the current block in the cache view
        /// </summary>
        private int _BlockNumber;
        public int BlockNumber {
            get { return _BlockNumber; }
            set { SetProperty(ref _BlockNumber, value); }
        }
        /// <summary>
        /// increments the number of blocks in the view
        /// </summary>
        private string _Increment;
        public string Increment {
            get { return _Increment; }
            set { SetProperty(ref _Increment, value); }
        }
        /// <summary>
        /// identifies which block correspondis to the requested address to be highylighted in the view
        /// </summary>
        private bool _IsBlockNumberChoosed;
        public bool IsBlockNumberChoosed
        {
            get { return _IsBlockNumberChoosed;}
            set { SetProperty(ref _IsBlockNumberChoosed,value); }
        }
        /// <summary>
        /// holds the cache algorithm explanations to be presented in yje view
        /// </summary>
        private string _TextContent;
        public string TextContent
        {
            get { return _TextContent; }
            set { SetProperty(ref _TextContent, value); }
        }
        #endregion

        #region constructure
        public PresentationModel(int blockNumber, string increment, bool isBlockNumberChoosed)
        {
            BlockNumber = blockNumber;
            Increment = increment;
            IsBlockNumberChoosed = isBlockNumberChoosed;
        }
        public PresentationModel(string textContent)
        {
            TextContent = textContent;
        }
        #endregion
    }
}
