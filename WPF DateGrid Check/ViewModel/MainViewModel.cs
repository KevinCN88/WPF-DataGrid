using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace WPF_DateGrid_Check.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        private bool _checkAll;

        /// <summary>
        /// ȫѡ
        /// </summary>
        public bool CheckAll
        {
            get { return _checkAll; }
            set
            {
                _checkAll = value;
                if (GridModelList.Count != 0)
                {
                    GridModelList.ToList().ForEach((ary) => ary.IsCheck = CheckAll);
                }
                RaisePropertyChanged();
            }
        }

        private string searchText = string.Empty;
        private RelayCommand _QueryCommand;

        private RelayCommand _AllDelCommand;
        private RelayCommand<MainModel> _DelCommand;
        private ObservableCollection<MainModel> _gridModelList = new ObservableCollection<MainModel>();




        /// <summary>
        /// ��������
        /// </summary>
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public ObservableCollection<MainModel> GridModelList
        {
            get { return _gridModelList; }
            set
            {
                _gridModelList = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        public RelayCommand QueryCommand
        {
            get
            {
                if (_QueryCommand == null)
                {
                    _QueryCommand = new RelayCommand(Query);
                }
                return _QueryCommand;
            }
            set { _QueryCommand = value; }
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        public RelayCommand AllDelCommand
        {
            get
            {
                if (_AllDelCommand == null)
                {
                    _AllDelCommand = new RelayCommand(AllDel);
                }
                return _AllDelCommand;
            }
            set { _AllDelCommand = value; }
        }
      


        /// <summary>
        /// ��ѯ
        /// </summary>
        private void Query()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    var queryList = GridModelList.Where(s => s.FunName.Contains(SearchText)).ToList();
                    if (queryList.Any())
                    {
                        GridModelList.Clear();
                        queryList.ForEach((ary=> GridModelList.Add(ary)));
                    }
                    
                }
                else
                {
                    InitUi();
                }
                    
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// ȷ��
        /// </summary>
        private void AllDel()
        {
            try
            {
                if (GridModelList.Count != 0)
                {

                    ////�жϽ��������Դ �Ƿ��й�ѡ������ 
                    //var notDelModel = GridModelList.Where(s => !s.IsCheck).ToList();
                    //GridModelList.Clear();
                    //if (notDelModel.Any())
                    //{  
                    //    notDelModel.ForEach((ary => GridModelList.Add(ary)));
                    //}  
                  var notDelModel=  GridModelList.Where(
                        s => s.DateTime.DayOfWeek == DayOfWeek.Saturday || s.DateTime.DayOfWeek == DayOfWeek.Sunday).ToList();
                    if (notDelModel.Any())
                    {
                        notDelModel.ForEach((ary) =>
                        {
                            ary.CheckBack = ary.IsCheck ? "#CCCCCC" : "#FC3E36";
                            ary.IsCheck = !ary.IsCheck;
                        });
                    }


                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            InitUi();
        }

        private void InitUi()
        {
            GridModelList = new ObservableCollection<MainModel>();
            for (int i = 0; i < 6; i++)
            {
                MainModel model = new MainModel()
                {
                    FunName = "���" + i,
                    DateTime =DateTime.Now.AddMonths(i+1),
                    Week= DateTime.Now.AddMonths(i+1).DayOfWeek,
                };
                GridModelList.Add(model);
            }
        }
    }

    /// <summary>
    /// model
    /// </summary>
    public  class MainModel : ViewModelBase
    {

        private bool _isCheck;
        /// <summary>
        /// ѡ��
        /// </summary>
        public bool IsCheck
        {
            get { return _isCheck; }
            set
            {
                _isCheck = value;
                RaisePropertyChanged();
            }
        }

        private string _funName;
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public string FunName
        {
            get { return _funName; }
            set
            {
                _funName = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _depict;
        /// <summary>
        /// ʱ��
        /// </summary>
        public DateTime DateTime
        {
            get { return _depict; }
            set
            {
                _depict = value;
                RaisePropertyChanged();
            }
        }
        private DayOfWeek _Week;
        /// <summary>
        /// ʱ��
        /// </summary>
        public DayOfWeek Week
        {
            get { return _Week; }
            set
            {
                _Week = value;
                RaisePropertyChanged();
            }
        }
        

        private string _checkBack = "#CCCCCC";
        /// <summary>
        /// ��ɫ
        /// </summary>
        public string CheckBack
        {
            get { return _checkBack; }
            set
            {
                _checkBack = value;
                RaisePropertyChanged();
            }
        }

       
    }
}