﻿#pragma checksum "C:\Users\00619461\Desktop\Treasury_Docs\RadControlsSilverlightClient\AccountsSigners.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "95AE231153E556B0D3ECE53848C5D675"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace RadControlsSilverlightClient {
    
    
    public partial class AccountsSigners : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Telerik.Windows.Controls.RadDataServiceDataSource dataServiceDataSource;
        
        internal Telerik.Windows.Controls.RadDataServiceDataSource AccountsdataServiceDataSource;
        
        internal Telerik.Windows.Controls.RadDataServiceDataSource SignersdataServiceDataSource;
        
        internal Telerik.Windows.Controls.RadDataServiceDataSource EntitlementsdataServiceDataSource;
        
        internal Telerik.Windows.Controls.RadDataServiceDataSource Accounts_Signers_EntitlementsDataSource;
        
        internal Telerik.Windows.Controls.RadBusyIndicator busyIndicator;
        
        internal Telerik.Windows.Controls.RadGridView RadGridView1;
        
        internal Telerik.Windows.Controls.RadDataPager gridPager;
        
        internal Telerik.Windows.Controls.RadBusyIndicator DataFormLoading;
        
        internal Telerik.Windows.Controls.RadDataForm accountSignersDataForm;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/RadControlsSilverlightClient;component/AccountsSigners.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.dataServiceDataSource = ((Telerik.Windows.Controls.RadDataServiceDataSource)(this.FindName("dataServiceDataSource")));
            this.AccountsdataServiceDataSource = ((Telerik.Windows.Controls.RadDataServiceDataSource)(this.FindName("AccountsdataServiceDataSource")));
            this.SignersdataServiceDataSource = ((Telerik.Windows.Controls.RadDataServiceDataSource)(this.FindName("SignersdataServiceDataSource")));
            this.EntitlementsdataServiceDataSource = ((Telerik.Windows.Controls.RadDataServiceDataSource)(this.FindName("EntitlementsdataServiceDataSource")));
            this.Accounts_Signers_EntitlementsDataSource = ((Telerik.Windows.Controls.RadDataServiceDataSource)(this.FindName("Accounts_Signers_EntitlementsDataSource")));
            this.busyIndicator = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("busyIndicator")));
            this.RadGridView1 = ((Telerik.Windows.Controls.RadGridView)(this.FindName("RadGridView1")));
            this.gridPager = ((Telerik.Windows.Controls.RadDataPager)(this.FindName("gridPager")));
            this.DataFormLoading = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("DataFormLoading")));
            this.accountSignersDataForm = ((Telerik.Windows.Controls.RadDataForm)(this.FindName("accountSignersDataForm")));
        }
    }
}

