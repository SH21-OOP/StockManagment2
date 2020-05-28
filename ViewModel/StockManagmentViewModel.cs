using BL.Abstract;
using BL.Impl;
using DAL.Impl;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ViewModel
{
    public class StockManagmentViewModel : INotifyPropertyChanged
    {
        private readonly IProductService _ProductService;
        private readonly IComercialProductGroupService _ComercialProductGroupService;

        private ObservableCollection<ComercialProductGroupDTO> _ProductGroups;
        private ObservableCollection<ProductDTO> _Products;


        public event PropertyChangedEventHandler PropertyChanged;


        public StockManagmentViewModel()
        {
            UnitOfWork _UnitOfWork = new UnitOfWork();
            _ProductService = new ProductService(_UnitOfWork);
            _ComercialProductGroupService = new ComercialProductGroupService(_UnitOfWork);

            _Products = new ObservableCollection<ProductDTO>();
            _ProductGroups = new ObservableCollection<ComercialProductGroupDTO>();

            RefreshProductGroups();
        }

        private void RefreshProductGroups()
        {
            _ProductGroups.Clear();
            foreach (var item in _ComercialProductGroupService.GetAll())
                _ProductGroups.Add(item);
            OnPropertyChanged(nameof(ProductGroups));
        }

        private void RefreshProducts()
        {
            _Products.Clear();
            foreach (var item in _ProductService.GetAll())
                if (item.ComercialProductGroupID == SelectedGroup.Id)
                    _Products.Add(item);
            OnPropertyChanged(nameof(Products));
        }

        public List<ComercialProductGroupDTO> ProductGroups
        {
            get { return _ProductGroups.ToList(); }
        }

        public List<ProductDTO> Products
        {
            get { return _Products.ToList(); }
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        // ########################## Commands

        private IMyCommand _CreateProductGroupCommand;
        public IMyCommand CreateProductGroupCommand
        {
            get
            {
                if (_CreateProductGroupCommand == null)
                    _CreateProductGroupCommand = new RelayCommand(this.CreateProductGroup, this.CanExecuteCreateProductGroup);
                return _CreateProductGroupCommand;
            }
            set
            {
                _CreateProductGroupCommand = value;
            }
        }
        private void CreateProductGroup(object o)
        {
            _ComercialProductGroupService.Add(
                new ComercialProductGroupDTO()
                {
                    Name = this.ProductGroupName,
                    PurchasePrice = this.ProductGroupPurchasePrice,
                    SellPrice = this.ProductGroupSellPrice,
                    DeliveryTime = this.ProductGroupDeliveryTime
                }
            );
            RefreshProductGroups();
        }

        private bool CanExecuteCreateProductGroup(object o)
        {
            return !String.IsNullOrEmpty(this.ProductGroupName) &&
                    ProductGroupPurchasePrice >= 0 &&
                    ProductGroupSellPrice >= 0 &&
                    ProductGroupDeliveryTime >= 0;
        }

        // ##

        private IMyCommand _DeleteProductGroupCommand;
        public IMyCommand DeleteProductGroupCommand
        {
            get
            {
                if (_DeleteProductGroupCommand == null)
                    _DeleteProductGroupCommand = new RelayCommand(this.DeleteProductGroup, this.CanExecuteDeleteProductGroup);
                return _DeleteProductGroupCommand;
            }
            set
            {
                _DeleteProductGroupCommand = value;
            }
        }
        private void DeleteProductGroup(object o)
        {
            _ComercialProductGroupService.Delete(SelectedGroup.Id);
            RefreshProductGroups();
        }

        private bool CanExecuteDeleteProductGroup(object o)
        {
            return SelectedGroup != null;
        }

        // ##

        private IMyCommand _UpdateProductGroupCommand;
        public IMyCommand UpdateProductGroupCommand
        {
            get
            {
                if (_UpdateProductGroupCommand == null)
                    _UpdateProductGroupCommand = new RelayCommand(this.UpdateProductGroup, this.CanExecuteUpdateProductGroup);
                return _UpdateProductGroupCommand;
            }
            set
            {
                _UpdateProductGroupCommand = value;
            }
        }

        private void UpdateProductGroup(object o)
        {
            SelectedGroup.Name = this.ProductGroupName;
            SelectedGroup.PurchasePrice = this.ProductGroupPurchasePrice;
            SelectedGroup.SellPrice = this.ProductGroupSellPrice;
            SelectedGroup.DeliveryTime = this.ProductGroupDeliveryTime;
            _ComercialProductGroupService.Update(SelectedGroup);
            RefreshProductGroups();
        }

        private bool CanExecuteUpdateProductGroup(object o)
        {
            return !String.IsNullOrEmpty(this.ProductGroupName) &&
                    ProductGroupPurchasePrice >= 0 &&
                    ProductGroupSellPrice >= 0 &&
                    ProductGroupDeliveryTime >= 0 &&
                    SelectedGroup.Id > 0;
        }
        // ##

        private IMyCommand _MarkAsEndsProductGroupCommand;
        public IMyCommand MarkAsEndsProductGroupCommand
        {
            get
            {
                if (_MarkAsEndsProductGroupCommand == null)
                    _MarkAsEndsProductGroupCommand = new RelayCommand(this.MarkAsEndsProductGroup, this.CanExecuteMarkAsEndsProductGroup);
                return _MarkAsEndsProductGroupCommand;
            }
            set
            {
                _MarkAsEndsProductGroupCommand = value;
            }
        }

        private void MarkAsEndsProductGroup(object o)
        {
            SelectedGroup.Ends = true;
            SelectedGroup.TermOfUse = ProductGroupTermOfUse;
            _ComercialProductGroupService.Update(SelectedGroup);
            RefreshProductGroups();
        }

        private bool CanExecuteMarkAsEndsProductGroup(object o)
        {
            return SelectedGroup.Id > 0 && ProductGroupTermOfUse > 0;
        }
        // ##

        private IMyCommand _MarkAsNotEndsProductGroupCommand;
        public IMyCommand MarkAsNotEndsProductGroupCommand
        {
            get
            {
                if (_MarkAsNotEndsProductGroupCommand == null)
                    _MarkAsNotEndsProductGroupCommand = new RelayCommand(this.MarkAsNotEndsProductGroup, this.CanExecuteMarkAsNotEndsProductGroup);
                return _MarkAsNotEndsProductGroupCommand;
            }
            set
            {
                _MarkAsNotEndsProductGroupCommand = value;
            }
        }

        private void MarkAsNotEndsProductGroup(object o)
        {
            SelectedGroup.TermOfUse = 0;
            SelectedGroup.Ends = false;
            _ComercialProductGroupService.Update(SelectedGroup);
            RefreshProductGroups();
        }

        private bool CanExecuteMarkAsNotEndsProductGroup(object o)
        {
            return SelectedGroup.Id > 0;
        }

        // ##

        private IMyCommand _CreateProductCommand;
        public IMyCommand CreateProductCommand
        {
            get
            {
                if (_CreateProductCommand == null)
                    _CreateProductCommand = new RelayCommand(this.CreateProduct, this.CanExecuteCreateProduct);
                return _CreateProductCommand;
            }
            set
            {
                _CreateProductCommand = value;
            }
        }

        private void CreateProduct(object o)
        {
            _ProductService.Add(
                new ProductDTO()
                {
                    Name = ProductName,
                    ComercialProductGroupID = SelectedGroup.Id,
                    Preparation = ProductPreparation,
                    Expiration = ProductExparation,
                    Quantity = ProductQuantity
                }
            );
            RefreshProducts();
        }

        private bool CanExecuteCreateProduct(object o)
        {
            return !string.IsNullOrEmpty(ProductName) &&
                    ProductQuantity > 0 &&
                    ProductPreparation < ProductExparation;
        }

        // ##

        private IMyCommand _DeleteProductCommand;
        public IMyCommand DeleteProductCommand
        {
            get
            {
                if (_DeleteProductCommand == null)
                    _DeleteProductCommand = new RelayCommand(this.DeleteProduct, this.CanExecuteDeleteProduct);
                return _DeleteProductCommand;
            }
            set
            {
                _DeleteProductCommand = value;
            }
        }

        private void DeleteProduct(object o)
        {
            _ProductService.Delete(SelectedProduct.Id);
            RefreshProducts();
        }

        private bool CanExecuteDeleteProduct(object o)
        {
            return SelectedProduct != null;
        }

        // ##

        private IMyCommand _UpdateProductCommand;
        public IMyCommand UpdateProductCommand
        {
            get
            {
                if (_UpdateProductCommand == null)
                    _UpdateProductCommand = new RelayCommand(this.UpdateProduct, this.CanExecuteUpdateProduct);
                return _UpdateProductCommand;
            }
            set
            {
                _UpdateProductCommand = value;
            }
        }

        private void UpdateProduct(object o)
        {
            SelectedProduct.Name = ProductName;
            SelectedProduct.Expiration = ProductExparation;
            SelectedProduct.Preparation = ProductPreparation;
            SelectedProduct.Quantity = ProductQuantity;
            _ProductService.Update(SelectedProduct);
            RefreshProducts();
        }

        private bool CanExecuteUpdateProduct(object o)
        {
            return SelectedProduct != null &&
                    !string.IsNullOrEmpty(ProductName) &&
                    ProductQuantity > 0 &&
                    ProductPreparation < ProductExparation;
        }

        // ########################## Properties

        private string _ProductGroupName;
        public string ProductGroupName
        {
            get => _ProductGroupName;
            set
            {
                _ProductGroupName = value;
                OnPropertyChanged(nameof(ProductGroupName));
                CreateProductGroupCommand.RaiseCanExecuteChanged();
            }
        }

        private int _ProductGroupPurchasePrice;
        public int ProductGroupPurchasePrice
        {
            get => _ProductGroupPurchasePrice;
            set
            {
                _ProductGroupPurchasePrice = value;
                OnPropertyChanged(nameof(ProductGroupPurchasePrice));
                CreateProductGroupCommand.RaiseCanExecuteChanged();
            }
        }

        private int _ProductGroupSellPrice;
        public int ProductGroupSellPrice
        {
            get => _ProductGroupSellPrice;
            set
            {
                _ProductGroupSellPrice = value;
                OnPropertyChanged(nameof(ProductGroupSellPrice));
                CreateProductGroupCommand.RaiseCanExecuteChanged();
            }
        }

        private int _ProductGroupDeliveryTime;
        public int ProductGroupDeliveryTime
        {
            get => _ProductGroupDeliveryTime;
            set
            {
                _ProductGroupDeliveryTime = value;
                OnPropertyChanged(nameof(ProductGroupDeliveryTime));
                CreateProductGroupCommand.RaiseCanExecuteChanged();
            }
        }

        private int _ProductGroupTermOfUse;
        public int ProductGroupTermOfUse
        {
            get => _ProductGroupTermOfUse;
            set
            {
                _ProductGroupTermOfUse = value;
                OnPropertyChanged(nameof(ProductGroupTermOfUse));
            }
        }

        private string _ProductName;
        public string ProductName
        {
            get => _ProductName;
            set
            {
                _ProductName = value;
                OnPropertyChanged(nameof(ProductName));
                CreateProductCommand.RaiseCanExecuteChanged();
                UpdateProductCommand.RaiseCanExecuteChanged();
            }
        }

        private int _ProductQuantity;
        public int ProductQuantity
        {
            get => _ProductQuantity;
            set
            {
                _ProductQuantity = value;
                OnPropertyChanged(nameof(ProductQuantity));
                CreateProductCommand.RaiseCanExecuteChanged();
                UpdateProductCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _ProductPreparation;
        public DateTime ProductPreparation
        {
            get => _ProductPreparation;
            set
            {
                _ProductPreparation = value;
                OnPropertyChanged(nameof(ProductPreparation));
                CreateProductCommand.RaiseCanExecuteChanged();
                UpdateProductCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _ProductExparation;
        public DateTime ProductExparation
        {
            get => _ProductExparation;
            set
            {
                _ProductExparation = value;
                OnPropertyChanged(nameof(ProductExparation));
                CreateProductCommand.RaiseCanExecuteChanged();
                UpdateProductCommand.RaiseCanExecuteChanged();
            }
        }

        private ProductDTO _SelectedProduct;
        public ProductDTO SelectedProduct
        {
            get => _SelectedProduct;
            set
            {
                _SelectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                DeleteProductCommand.RaiseCanExecuteChanged();
            }
        }

        private ComercialProductGroupDTO _SelectedGroup;
        public ComercialProductGroupDTO SelectedGroup
        {
            get => _SelectedGroup;
            set
            {
                _SelectedGroup = value;
                OnPropertyChanged(nameof(SelectedGroup));
                DeleteProductGroupCommand.RaiseCanExecuteChanged();
                UpdateProductGroupCommand.RaiseCanExecuteChanged();
                MarkAsEndsProductGroupCommand.RaiseCanExecuteChanged();
                MarkAsNotEndsProductGroupCommand.RaiseCanExecuteChanged();
                RefreshProducts();
            }
        }
    }
}
