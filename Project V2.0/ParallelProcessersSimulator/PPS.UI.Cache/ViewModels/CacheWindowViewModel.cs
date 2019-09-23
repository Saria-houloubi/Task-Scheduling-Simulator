using PPS.UI.Shared.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using PPS.UI.Cache.Models;
using Prism.Commands;

namespace PPS.UI.Cache.ViewModels
{
    /// <summary>
    /// The view model and the logic for the <see cref="CacheWindow"/>
    /// </summary>
    public class CacheWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// an instance of the cache model class
        /// </summary>
        public CacheModel CacheModel { get; private set; }
        /// <summary>
        /// an instance of the ValueConverter class
        /// </summary>
        public ValueConverterModel ValueConverterModel { get; private set; }
        #region Properties
        /// <summary>
        /// The selected cache size value from <see cref="CacheSize"/>
        /// </summary>
        private string _SelectedCacheSize;
        public string SelectedCacheSize
        {
            get { return _SelectedCacheSize; }
            set
            {
                SetProperty(ref _SelectedCacheSize, value);
                VisibiltyStatues = false;                
            }
        }
        private List<string> _CacheSizes;
        public List<string> CacheSizes
        {
            get { return _CacheSizes; }
            set { SetProperty(ref _CacheSizes, value); }
        }
        /// <summary>
        /// the selected block size from <see cref="SelectedBlockSizes"/>
        /// </summary>
        private string _SelectedBlockSize;
        public string SelectedBlockSize
        {
            get { return _SelectedBlockSize; }
            set
            {
                SetProperty(ref _SelectedBlockSize, value);
                VisibiltyStatues = false;                
            }
        }
        private List<string> _BlockSizes;
        public List<string> BlockSizes
        {
            get { return _BlockSizes; }
            set { SetProperty(ref _BlockSizes, value); }
        }
        /// <summary>
        /// the selected associativity type from<see cref="Associativites"/>
        /// </summary>
        private string _SelectedAssociativity;
        public string SelectedAssociativity
        {
            get { return _SelectedAssociativity; }
            set
            {
                SetProperty(ref _SelectedAssociativity, value);
                IndexTagDictionary.Clear();
                VisibiltyStatues = false;                
            }
        }
        private List<string> _Associativivties;
        public List<string> Associativities
        {
            get { return _Associativivties; }
            set { SetProperty(ref _Associativivties, value);}
        }
        /// <summary>
        /// the selected ram size from <see cref="RamSizes"/>
        /// </summary>
        private string _SelectedRamSize;
        public string SelectedRamSize
        {
            get { return _SelectedRamSize; }
            set
            {
                SetProperty(ref _SelectedRamSize, value);
                VisibiltyStatues = false;
                IsRamSizeSelected();           
            }
        }
        private List<string> _RamSizes;
        public List<string> RamSizes
        {
            get { return _RamSizes; }
            set { SetProperty(ref _RamSizes, value); }
        }
        /// <summary>
        /// a string that holds the offset bits in the requested address
        /// </summary>
        private string _Offset;
        public string Offset
        {
            get { return _Offset; }
            set { SetProperty(ref _Offset, value); }
        }
        /// <summary>
        ///  a string that holds the index bits in the requested address
        /// </summary>
        private string _Index;
        public string Index
        {
            get { return _Index; }
            set { SetProperty(ref _Index, value); }
        }
        /// <summary>
        ///  a string that holds the tag bits in the requested address
        /// </summary>
        private string _Tag;
        public string Tag
        {
            get { return _Tag; }
            set { SetProperty(ref _Tag, value); }
        }
        /// <summary>
        ///total number of bits in the requested address
        /// </summary>
        private int _TotalNumberOfBits;
        public int TotalNumberOfBits
        {
            get { return _TotalNumberOfBits; }
            set { SetProperty(ref _TotalNumberOfBits, value); }
        }
        /// <summary>
        /// holds the value of the index bits in decimal form
        /// </summary>
        private int _IndexInDecimal = 0;
        public int IndexInDecimal
        {
            get { return _IndexInDecimal; }
            set { SetProperty(ref _IndexInDecimal, value); }
        }
        /// <summary>
        /// holds value 'Hit' if it is a hit, or 'Miss' if it is a miss
        /// </summary>
        private string _HitOrMiss;
        public string HitOrMiss
        {
            get { return _HitOrMiss; }
            set { SetProperty(ref _HitOrMiss, value); }
        }
        /// <summary>
        ///holds the integer value representing the size of the ram
        /// </summary>
        private int _ramSizeToInt;
        public int RamSizeToInt
        {
            get { return _ramSizeToInt; }
            set { SetProperty(ref _ramSizeToInt, value);

            }
        }
        /// <summary>
        /// holds the requested address of the cache
        /// </summary>
        private string _RequestAddress = "";
        public string RequestAddress
        {
            get { return _RequestAddress; }
            set
            {
                SetProperty(ref _RequestAddress, value);
                ValidateInput();
            }
        }
        /// <summary>
        /// defines if the user can enter the requested address depending on wether the ram size has been chosen <see cref="SelectedRamSize"/>
        /// </summary>
        private bool _CanWrite;
        public bool CanWrite
        {
            get { return _CanWrite; }
            set { SetProperty(ref _CanWrite, value); }
        }
 
        #region boolean properties related to radio button in the window
        /// <summary>
        /// defines if the cache algorithm is Direct Mapping or Set Associativity
        /// </summary>
        private bool _IsDirectMapping = true;
        public bool IsDirectMapping
        {
            get { return _IsDirectMapping; }
            set
            {
                SetProperty(ref _IsDirectMapping, value);
                VisibiltyStatues = false;
            }
        }
        /// <summary>
        /// defines if the requested addres is in tge binary or hexadecimal form
        /// </summary>
        private bool _IsBinary = true;
        public bool IsBinary
        {
            get { return _IsBinary; }
            set
            {
                SetProperty(ref _IsBinary, value);
                // when the user chooses the binary form that means this property becomes true, the requested address convert to binary using the HexadecimalToBinary function
                if (value) { RequestAddress = HexadecimalToBinary(RequestAddress); }
                // when the user chooses the hexadecimal form that means this property becomes false, the requested address convert to hexadecimal using the BinaryToHexadecimal function
                else { RequestAddress = BinaryToHexadecimal(RequestAddress); }
            }
        }
        #endregion

        #region properties related to hit and miss 
        /// <summary>
        /// a dictionary that holds all the index_tag values to define if the requested address is a hit ,in case the tag and index bits of requested address matche the tag and index bits in the cache.
        /// or if it is a miss, in case the tag and index bits of requested address do not matche the tag and index bits in the cache.
        /// </summary>
        private Dictionary<string, string> _IndexTagDictionary;
        public Dictionary<string, string> IndexTagDictionary
        {
            get { return _IndexTagDictionary; }
            set { SetProperty(ref _IndexTagDictionary, value); }
        }
        #endregion

        #region presentation properties
        private string _FirstColumnName;
        public string FirstColumnName
        {
            get { return _FirstColumnName; }
            set { SetProperty(ref _FirstColumnName, value); }
        }
        /// <summary>
        /// holds all the valide Blocks in the cache
        /// </summary>
        public ObservableCollection<PresentationModel> CacheBlocksPresented
        {
            get; set;
        }
        /// <summary>
        /// holds the cache algorithm demonstrations
        /// </summary>
        public ObservableCollection<PresentationModel> Demonstrations { get; set; }
        /// <summary>
        ///this property controls the visibility of the stack panel that contains the demonstration of the mathematic rules of cache algorthim 
        /// </summary>
        private bool _VisibiltyStatues;
        public bool VisibiltyStatues
        {
            get { return _VisibiltyStatues; }
            set { SetProperty(ref _VisibiltyStatues, value); }
        }
        #endregion

        #endregion

        #region Commands
        public DelegateCommand AddressCalculatingCommand { get; set; }
        #endregion

        #region Command methods
        /// <summary>
        /// The function that will be called once the <see cref="AddressCalculatingCommand"/> is invoked
        /// calculates the tag,index/set,offset bits from the requested address
        /// </summary>
        public void AddressCalculatingCommandExecute()
        {
            BoolHitOrMiss = false;
            string tempAddress = RequestAddress;
            OnCacheAlgorithimStart();
            if (tempAddress.Length < TotalNumberOfBits) { tempAddress = tempAddress.PadLeft(TotalNumberOfBits, '0'); }
            if (!IsBinary) { tempAddress = HexadecimalToBinary(tempAddress); }            
            // holds the integer value representing the size of the cache
            int cacheSizeValue = int.Parse(RegexFunction(SelectedCacheSize, true)) * ValueConverterModel.ConvertMemorySizeTypeToByte(RegexFunction(SelectedCacheSize, false));
            //holds the integer value representing the size of the blocks
            int blockSizeValue = int.Parse(RegexFunction(SelectedBlockSize, true));
            //the number of blocks in the cache
            int numberOfBlocks = cacheSizeValue / blockSizeValue;                
            // the number of bits int the index part of the requested address
            int numberOfIndexBits = 0;
            //the number of bits the tag part of the requested address
            int numberOfTagBits = 0;
            // the number of bits int he index part of the requested address in the set associativity algorithm type
            int numberOfIndexBitsSetAssociativity = 0;
            //the number of bits the tag part of the requested address  in the set associativity algorithm type
            int numberOfTagBitsSetAssociativity = 0;
            //holds the integer value representing the type of associativity
            int associativityTypeValue =0;
            int numberOfSets =0;
            if (IsDirectMapping)
            {
                FirstColumnName = "Block Number";                
                 numberOfIndexBits = GetLogValue(numberOfBlocks);                
                 numberOfTagBits = GetLogValue(RamSizeToInt) - GetLogValue(blockSizeValue) - numberOfIndexBits;            
                BinaryToDecimal_TagIndexOffsetAssignmentnFunction(tempAddress, numberOfBlocks, blockSizeValue,numberOfIndexBits,numberOfTagBits, cacheSizeValue);
            }
            else
            {
                FirstColumnName = "Set Number";
                associativityTypeValue = int.Parse(RegexFunction(SelectedAssociativity, true));                
                numberOfSets = numberOfBlocks / associativityTypeValue;                
                numberOfIndexBitsSetAssociativity = GetLogValue(numberOfSets);                
                numberOfTagBitsSetAssociativity = GetLogValue(RamSizeToInt) - GetLogValue(blockSizeValue) - numberOfIndexBitsSetAssociativity;               
                BinaryToDecimal_TagIndexOffsetAssignmentnFunction( tempAddress, numberOfSets, blockSizeValue, numberOfIndexBitsSetAssociativity, numberOfTagBitsSetAssociativity, cacheSizeValue);
            }
            HitOrMissFunction();          
            CanWrite = true;
        }
        /// <summary>
        /// The function to check if the <see cref="AddressCalculatingCommandExecute"/> can get executed       
        /// </summary>
        /// <returns></returns>
        public bool AddressCalculatingCommandCanExecute()
        { return ((!string.IsNullOrEmpty(SelectedBlockSize) && !string.IsNullOrEmpty(SelectedCacheSize) && !string.IsNullOrEmpty(SelectedRamSize) && !string.IsNullOrEmpty(RequestAddress)) && ((!IsDirectMapping && !string.IsNullOrEmpty(SelectedAssociativity)) || IsDirectMapping)); }
        #endregion

        #region Constructer    
        public CacheWindowViewModel()
        {
            AddressCalculatingCommand = new DelegateCommand(AddressCalculatingCommandExecute, AddressCalculatingCommandCanExecute).ObservesProperty(() => SelectedBlockSize).ObservesProperty(() => SelectedCacheSize).ObservesProperty(() => SelectedAssociativity).ObservesProperty(() => SelectedRamSize).ObservesProperty(() => RequestAddress);
            InitiliazeData();
        }
        public void InitiliazeData()
        {
            CacheModel = new CacheModel();
            ValueConverterModel = new ValueConverterModel();
            IndexTagDictionary = new Dictionary<string, string>();
            CacheBlocksPresented = new ObservableCollection<PresentationModel>();
            Demonstrations = new ObservableCollection<PresentationModel>();
            FillCacheSize();
            FillBlockSize();
            FillAssociativityType();
            FillRamSize();           
        }
        #endregion

        #region Cache algorithm
        public bool BoolHitOrMiss;
        /// <summary>
        /// initialize the algorithm related porperties
        /// </summary>
        public void OnCacheAlgorithimStart()
        {
            Offset = "";
            Index = "";
            Tag = "";
            VisibiltyStatues = true;
            CacheBlocksPresented.Clear();
            Demonstrations.Clear();
        }
        /// <summary>
        /// specify if it is a hit or miss
        /// </summary>
        public void HitOrMissFunction()
        {
           string tempTag = Tag.PadLeft(24, '0');
            foreach (var entry in IndexTagDictionary)
            {
                if (tempTag == entry.Value && Index == entry.Key)

                {
                    BoolHitOrMiss = true;
                    break;
                }
            }
            if (BoolHitOrMiss)
                HitOrMiss = "Hit";
            else
            {
                HitOrMiss = "Miss";
                IndexTagDictionary[Index] = tempTag;
            }
            BoolHitOrMiss = false;
        }
        /// <summary>
        /// this value is used to give the tag,index and offset the correct value then convert the index value to decimal and prepair the values of the listview to be printed
        /// </summary>
        /// <param name="address"></param>
        /// <param name="numberOfBlocksOrSets"></param>
        /// <param name="blockSizeValue"></param>
        /// <param name="numberOfIndexBits"></param>
        /// <param name="numberOfTagBits"></param>
        /// <param name="cacheSizeValue"></param>
        public void BinaryToDecimal_TagIndexOffsetAssignmentnFunction(string address, int numberOfBlocksOrSets,int blockSizeValue,int numberOfIndexBits,int numberOfTagBits,int cacheSizeValue)
        {            
            Tag = address.Substring(address.Length - GetLogValue(blockSizeValue) - numberOfIndexBits - numberOfTagBits, numberOfTagBits);
            Index = address.Substring(address.Length - GetLogValue(blockSizeValue) - numberOfIndexBits, numberOfIndexBits);
            Offset = address.Substring(address.Length - GetLogValue(blockSizeValue), GetLogValue(blockSizeValue));
            IndexInDecimal = 0;
            int x = 0;
            for (int i = Index.Length - 1; i >= 0; i--, x++)
            {
                if (Index[i] == '1') IndexInDecimal += (int)Math.Pow(2, x);
            }
            #region Cache Blocks Values
            for (int i = 0; i < numberOfBlocksOrSets; i++)
            {
                string BlockRow = i.ToString() + ", " + (i + numberOfBlocksOrSets).ToString() + ", " + (i + numberOfBlocksOrSets * 2).ToString() + ", " + (i + numberOfBlocksOrSets * 3).ToString() + ", " + (i + numberOfBlocksOrSets * 4).ToString() + ", " + (i + numberOfBlocksOrSets * 5).ToString() + " ," + (i + numberOfBlocksOrSets * 6).ToString()+ ".......";           
                if (i == IndexInDecimal)
                    CacheBlocksPresented.Add(new PresentationModel(i, BlockRow, true));
                else
                    CacheBlocksPresented.Add(new PresentationModel(i, BlockRow, false));         
            }
            #endregion
            
            #region Cache Algorithm Explanations
            Demonstrations.Add(new PresentationModel("the Tag Bits are compared with the corresponding Tag Bits in the Cache Directory."));
            Demonstrations.Add(new PresentationModel("the Index Bits are used to select a particular Set in the Cache."));
            Demonstrations.Add(new PresentationModel("the Offest Bits are used to select a particular byte in the accessed block."));
            Demonstrations.Add(new PresentationModel("Memory Size= " + SelectedRamSize + " =2 to the power of " + TotalNumberOfBits));
            Demonstrations.Add(new PresentationModel("Cache Size= " + SelectedCacheSize + " =2 to the power of " + GetLogValue(cacheSizeValue)));
            Demonstrations.Add(new PresentationModel("Block Size= " + SelectedBlockSize + " =2 to the power of " + GetLogValue(blockSizeValue)));
            Demonstrations.Add(new PresentationModel("Number of bits in Tag = Total bits - Index bits - Offset bits= " + TotalNumberOfBits + "- " + GetLogValue(numberOfBlocksOrSets) + "- " + GetLogValue(blockSizeValue) + "= " + numberOfTagBits));
            if (IsDirectMapping) Demonstrations.Add(new PresentationModel("Number of blocks in cache=" + SelectedCacheSize + "/" + SelectedBlockSize + " = (2 to the power of" + GetLogValue(cacheSizeValue) + ")/(2 to the power of " + GetLogValue(blockSizeValue) + ")= " + numberOfIndexBits));
            else Demonstrations.Add(new PresentationModel("Number of sets in cache = Number of blocks/associativity type = (" + GetLogValue(numberOfBlocksOrSets) + ")/(" + SelectedAssociativity + ")= " + numberOfBlocksOrSets));
            #endregion
        }
        #endregion

        #region Helpers
        #region Fill Functions
        /// <summary>
        /// a function to fill all the values of the cache sizes that will be demonstraited so the user can choose one value
        /// </summary>
        private void FillCacheSize()
        {
            CacheSizes = new List<string>();
            foreach (var item in CacheModel.CacheSizes)
            { CacheSizes.Add(ValueConverterModel.ConvertToString(item)); }
        }
        /// <summary>
        /// a function to fill all the values of the block sizes that will be demonstraited so the user can choose one value
        /// </summary>
        private void FillBlockSize()
        {
            BlockSizes = new List<string>();
            foreach (var item in CacheModel.BlockSizes)
                BlockSizes.Add(ValueConverterModel.ConvertToString(item));
        }
        /// <summary>
        /// a function to fill all the values of the associativity type that will be demonstraited so the user can choose an associativity type
        /// </summary>
        private void FillAssociativityType()
        {
            Associativities = new List<string>();
            foreach (var item in CacheModel.Associativities)
                Associativities.Add(ValueConverterModel.ConvertToWays(item));
        }
        /// <summary>
        /// a function to fill all the values of the ram sizes that will be demonstraited so the user can choose one value
        /// </summary>
        private void FillRamSize()
        {
            RamSizes = new List<string>();
            foreach (var item in CacheModel.RamSizes)
            { RamSizes.Add(ValueConverterModel.ConvertToString(item)); }
        }

        #endregion

        /// <summary>
        /// when ram size been selected the user can enter the address
        /// </summary>
        private void IsRamSizeSelected()
        {
            CanWrite = true;
            RamSizeToInt = int.Parse(RegexFunction(SelectedRamSize, true)) * ValueConverterModel.ConvertMemorySizeTypeToByte(RegexFunction(SelectedRamSize, false));
            TotalNumberOfBits = GetLogValue(RamSizeToInt);
        }

        /// <summary>
        /// return the binary logarthim of a number
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        private int GetLogValue(int Value)
        {
            return (int)Math.Log(Value, 2);
        }
        /// <summary>
        /// obtaining the numbers within the first argument if second argument is false
        /// remove the numbers within the first argument if the second argument is true
        /// </summary>
        /// <param name="stringToBeAnalysed"></param>
        /// <param name="getNumber">indicates wether we want to get the number or not</param>
        /// <returns></returns>
        private string RegexFunction(string stringToBeAnalysed, bool getNumber)
        {
            if (!getNumber) return (Regex.Replace(stringToBeAnalysed, @"[\d-]", string.Empty));
            return (Regex.Match(stringToBeAnalysed, @"\d+").Value);
        }
        /// <summary>
        /// convert the hexadecimal value of the address to equivalent binary value
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private string HexadecimalToBinary(string address)
        {
            string tempAddress = "";
            address = address.ToLower();
            if (!string.IsNullOrEmpty(address))
            {
                for (int i = address.Length - 1; i >= 0; i--)
                {
                    tempAddress += ValueConverterModel.GetHexadecimalValue(address[i].ToString());
                }
                address = "";
                int subStringEnding = tempAddress.Length;
                while (subStringEnding >= 4)
                {
                    address += tempAddress.Substring(subStringEnding - 4, 4);
                    subStringEnding -= 4;
                }
                return address;
            }
            else
                return null;
        }
        /// <summary>
        /// convert the binary value of the address to equivalent hexadecimal value
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private string BinaryToHexadecimal(string address)
        {
            string tempAddress = "";
            if (address.Length % 4 != 0)
            {
                int closestFourMultiple = 4 * (address.Length / 4 + 1);
                address = address.PadLeft(closestFourMultiple, '0');
            }            
            if (!string.IsNullOrEmpty(address))
            {
                int subStringBegining = 0;
                string subStringHolder;
                while (subStringBegining < address.Length)
                {    //change each four characters -represinting binary values- to their hexadecimal equvlant             
                    subStringHolder = address.Substring(subStringBegining, 4);
                    tempAddress += ValueConverterModel.GetHexadecimalValue(subStringHolder);
                    subStringBegining += 4;
                }
                return tempAddress;
            }
            else return null;
        }
        /// <summary>
        /// check if the entered address is correct
        /// </summary>
        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(RequestAddress))
            {               
                int len = RequestAddress.Length;
                int lengthDifference = len - TotalNumberOfBits;
                if (IsBinary)
                {  //check if the binary requested address does not contain hexcadecimal or any other characters in it
                    if (!string.IsNullOrEmpty(RequestAddress))
                    {
                        for (int i = 0; i < len; i++)
                        {
                            if (RequestAddress[i] > '1' || RequestAddress[i] < '0')
                            {
                                RequestAddress = RequestAddress.Remove(i, 1);
                                len = RequestAddress.Length;
                            }
                        }
                    }
                    //check the size of the requested address in the binary form
                    if (RequestAddress.Length > TotalNumberOfBits)
                    {
                        RequestAddress = RequestAddress.Remove(len - lengthDifference, lengthDifference);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(RequestAddress))
                    {
                        for (int i = 0; i < len; i++)
                        {
                            if (RequestAddress[i] >= '0' && RequestAddress[i] <= '9') continue;
                            if (RequestAddress[i] >= 'a' && RequestAddress[i] <= 'f') continue;
                            if (RequestAddress[i] >= 'A' && RequestAddress[i] <= 'F') continue;
                            RequestAddress = RequestAddress.Remove(i, 1);
                            len = RequestAddress.Length;
                        }
                    }
                    int extraBits = TotalNumberOfBits % 4;
                    if (extraBits > 0) extraBits = 1;
                    if (RequestAddress.Length > (TotalNumberOfBits / 4 + extraBits))
                    {
                        lengthDifference = len - (TotalNumberOfBits / 4 + extraBits);
                        RequestAddress = RequestAddress.Remove(len - lengthDifference, lengthDifference);
                    }
                }

            }
        }
        #endregion
    }
}
