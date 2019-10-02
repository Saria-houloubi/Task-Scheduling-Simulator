using PPS.UI.Shared.Models.Base;
using System.Collections.Generic;
namespace PPS.UI.Cache.Models
{
  public class CacheModel: BaseNotifyModel
    {         
        #region constructor
        public CacheModel()
        {
            InitiliazeData();
        }
        /// <summary>
        /// a function to initializ data in the model class
        /// </summary>
        private void InitiliazeData()
        {                                               
                FillCacheSize();
                FillBlockSize();
                FillAssociativity();
                FillRamSize();  
        }
        #endregion

        #region lists

        /// <summary>
        /// containes all the  the cache sizes that will be used
        /// </summary>
        private List<int> _cacheSizes;
        public List<int> CacheSizes
        {
            get { return _cacheSizes; }
            set { SetProperty(ref _cacheSizes, value); }
        }
        /// <summary>
        /// containes all the  the block sizes that will be used
        /// </summary>
        private List<int> _blockSizes;
        public List<int> BlockSizes
        {
            get { return _blockSizes; }
            set { SetProperty(ref _blockSizes, value); }
        }
        /// <summary>
        /// containes all the  the associativity types sizes that will be used
        /// </summary>
        private List<int> _associativities;
        public List<int> Associativities
        {
            get { return _associativities; }
            set { SetProperty(ref _associativities, value); }
        }
        /// <summary>
        /// containes all the  the ram sizes that will be used
        /// </summary>
        private List<int> _ramSizes;
        public List<int> RamSizes
        {
            get { return _ramSizes; }
            set { SetProperty(ref _ramSizes, value); }
        }
        #endregion

        #region Helper
        /// <summary>
        /// a function to fill all the values of the cache sizes that will be be used in the cacheViewModel class
        /// </summary>
        private void FillCacheSize()
        {
            CacheSizes = new List<int>();
            CacheSizes.Add(128);//128 bytes
            CacheSizes.Add(512);//512 bytes
            CacheSizes.Add(1024);//1024 bytes = 1Kbyte         
        }
        /// <summary>
        /// a function to fill all the values of the block sizes that will be be used in the cacheViewModel class
        /// </summary>
        private void FillBlockSize()
        {
            BlockSizes = new List<int>();
            //all sizes are in byte
            BlockSizes.Add(2);
            BlockSizes.Add(4);
            BlockSizes.Add(8);
            BlockSizes.Add(16);
            BlockSizes.Add(32);
            BlockSizes.Add(64);
            BlockSizes.Add(256);
            BlockSizes.Add(512);

        }
        /// <summary>
        /// a function to fill all the associativity types that will be be used in the cacheViewModel class
        /// </summary>
        private void FillAssociativity()
        {
            Associativities = new List<int>();
            Associativities.Add(2);
            Associativities.Add(4);
            Associativities.Add(8);
            Associativities.Add(16);
        }
        /// <summary>
        /// a function to fill all the values of the ram sizes that will be be used in the cacheViewModel class
        /// </summary>
        private void FillRamSize()
        {
            RamSizes = new List<int>();
            RamSizes.Add(65536);//64 Kbyte
            RamSizes.Add(262144);//256 Kbyte
            RamSizes.Add(524288);//512 Kbyte
            RamSizes.Add(1048576);// 1 Mbyte           
            RamSizes.Add(16777216);// 16 Mbyte                       
        }
        #endregion

    }
}
