﻿#pragma checksum "..\..\..\View\Registreren.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F66A4F066B9DAEA34655D68428D5A246"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using nmct.ba.cashlessproject.ba.kassa.klant.ViewModel;
using nmct.ba.cashlessproject.ba.kassa.klant.converter;


namespace nmct.ba.cashlessproject.ba.kassa.klant.View {
    
    
    /// <summary>
    /// Registreren
    /// </summary>
    public partial class Registreren : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 62 "..\..\..\View\Registreren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtkaart;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\View\Registreren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtrfid;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\View\Registreren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox focus;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\View\Registreren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar progress;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\View\Registreren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtFoutmelding;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/nmct.ba.cashlessproject.ba.kassa.klant;component/view/registreren.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\Registreren.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 60 "..\..\..\View\Registreren.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtkaart = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.txtrfid = ((System.Windows.Controls.TextBox)(target));
            
            #line 63 "..\..\..\View\Registreren.xaml"
            this.txtrfid.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtrfid_KeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.focus = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.progress = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 6:
            this.txtFoutmelding = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

