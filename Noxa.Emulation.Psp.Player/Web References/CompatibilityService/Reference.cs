﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.312
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.312.
// 
#pragma warning disable 1591

namespace Noxa.Emulation.Psp.Player.CompatibilityService {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.312")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ServiceSoap", Namespace="http://psp.ixtli.com/pspcompat/")]
    public partial class Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ListGamesOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetGameOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddGameOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddReleaseOperationCompleted;
        
        private System.Threading.SendOrPostCallback CheckCompatibilityOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Service() {
            this.Url = global::Noxa.Emulation.Psp.Player.Properties.Settings.Default.Noxa_Emulation_Psp_Player_Noxa_Emulation_Psp_CompatabilityService_Service;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ListGamesCompletedEventHandler ListGamesCompleted;
        
        /// <remarks/>
        public event GetGameCompletedEventHandler GetGameCompleted;
        
        /// <remarks/>
        public event AddGameCompletedEventHandler AddGameCompleted;
        
        /// <remarks/>
        public event AddReleaseCompletedEventHandler AddReleaseCompleted;
        
        /// <remarks/>
        public event CheckCompatibilityCompletedEventHandler CheckCompatibilityCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://psp.ixtli.com/pspcompat/ListGames", RequestNamespace="http://psp.ixtli.com/pspcompat/", ResponseNamespace="http://psp.ixtli.com/pspcompat/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Game[] ListGames() {
            object[] results = this.Invoke("ListGames", new object[0]);
            return ((Game[])(results[0]));
        }
        
        /// <remarks/>
        public void ListGamesAsync() {
            this.ListGamesAsync(null);
        }
        
        /// <remarks/>
        public void ListGamesAsync(object userState) {
            if ((this.ListGamesOperationCompleted == null)) {
                this.ListGamesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnListGamesOperationCompleted);
            }
            this.InvokeAsync("ListGames", new object[0], this.ListGamesOperationCompleted, userState);
        }
        
        private void OnListGamesOperationCompleted(object arg) {
            if ((this.ListGamesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ListGamesCompleted(this, new ListGamesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://psp.ixtli.com/pspcompat/GetGame", RequestNamespace="http://psp.ixtli.com/pspcompat/", ResponseNamespace="http://psp.ixtli.com/pspcompat/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Game GetGame(long gameId) {
            object[] results = this.Invoke("GetGame", new object[] {
                        gameId});
            return ((Game)(results[0]));
        }
        
        /// <remarks/>
        public void GetGameAsync(long gameId) {
            this.GetGameAsync(gameId, null);
        }
        
        /// <remarks/>
        public void GetGameAsync(long gameId, object userState) {
            if ((this.GetGameOperationCompleted == null)) {
                this.GetGameOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetGameOperationCompleted);
            }
            this.InvokeAsync("GetGame", new object[] {
                        gameId}, this.GetGameOperationCompleted, userState);
        }
        
        private void OnGetGameOperationCompleted(object arg) {
            if ((this.GetGameCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetGameCompleted(this, new GetGameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://psp.ixtli.com/pspcompat/AddGame", RequestNamespace="http://psp.ixtli.com/pspcompat/", ResponseNamespace="http://psp.ixtli.com/pspcompat/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public AddResult AddGame(string username, string password, GameRelease release, [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] icon) {
            object[] results = this.Invoke("AddGame", new object[] {
                        username,
                        password,
                        release,
                        icon});
            return ((AddResult)(results[0]));
        }
        
        /// <remarks/>
        public void AddGameAsync(string username, string password, GameRelease release, byte[] icon) {
            this.AddGameAsync(username, password, release, icon, null);
        }
        
        /// <remarks/>
        public void AddGameAsync(string username, string password, GameRelease release, byte[] icon, object userState) {
            if ((this.AddGameOperationCompleted == null)) {
                this.AddGameOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddGameOperationCompleted);
            }
            this.InvokeAsync("AddGame", new object[] {
                        username,
                        password,
                        release,
                        icon}, this.AddGameOperationCompleted, userState);
        }
        
        private void OnAddGameOperationCompleted(object arg) {
            if ((this.AddGameCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddGameCompleted(this, new AddGameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://psp.ixtli.com/pspcompat/AddRelease", RequestNamespace="http://psp.ixtli.com/pspcompat/", ResponseNamespace="http://psp.ixtli.com/pspcompat/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public AddResult AddRelease(string username, string password, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] System.Nullable<long> gameId, GameRelease release, [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] icon) {
            object[] results = this.Invoke("AddRelease", new object[] {
                        username,
                        password,
                        gameId,
                        release,
                        icon});
            return ((AddResult)(results[0]));
        }
        
        /// <remarks/>
        public void AddReleaseAsync(string username, string password, System.Nullable<long> gameId, GameRelease release, byte[] icon) {
            this.AddReleaseAsync(username, password, gameId, release, icon, null);
        }
        
        /// <remarks/>
        public void AddReleaseAsync(string username, string password, System.Nullable<long> gameId, GameRelease release, byte[] icon, object userState) {
            if ((this.AddReleaseOperationCompleted == null)) {
                this.AddReleaseOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddReleaseOperationCompleted);
            }
            this.InvokeAsync("AddRelease", new object[] {
                        username,
                        password,
                        gameId,
                        release,
                        icon}, this.AddReleaseOperationCompleted, userState);
        }
        
        private void OnAddReleaseOperationCompleted(object arg) {
            if ((this.AddReleaseCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddReleaseCompleted(this, new AddReleaseCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://psp.ixtli.com/pspcompat/CheckCompatibility", RequestNamespace="http://psp.ixtli.com/pspcompat/", ResponseNamespace="http://psp.ixtli.com/pspcompat/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CompatibilityResults CheckCompatibility(string discId, ComponentInfo[] components) {
            object[] results = this.Invoke("CheckCompatibility", new object[] {
                        discId,
                        components});
            return ((CompatibilityResults)(results[0]));
        }
        
        /// <remarks/>
        public void CheckCompatibilityAsync(string discId, ComponentInfo[] components) {
            this.CheckCompatibilityAsync(discId, components, null);
        }
        
        /// <remarks/>
        public void CheckCompatibilityAsync(string discId, ComponentInfo[] components, object userState) {
            if ((this.CheckCompatibilityOperationCompleted == null)) {
                this.CheckCompatibilityOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckCompatibilityOperationCompleted);
            }
            this.InvokeAsync("CheckCompatibility", new object[] {
                        discId,
                        components}, this.CheckCompatibilityOperationCompleted, userState);
        }
        
        private void OnCheckCompatibilityOperationCompleted(object arg) {
            if ((this.CheckCompatibilityCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckCompatibilityCompleted(this, new CheckCompatibilityCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.312")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://psp.ixtli.com/pspcompat/")]
    public partial class Game {
        
        private long gameIDField;
        
        private string titleField;
        
        private string websiteField;
        
        private GameRelease[] releasesField;
        
        /// <remarks/>
        public long GameID {
            get {
                return this.gameIDField;
            }
            set {
                this.gameIDField = value;
            }
        }
        
        /// <remarks/>
        public string Title {
            get {
                return this.titleField;
            }
            set {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        public string Website {
            get {
                return this.websiteField;
            }
            set {
                this.websiteField = value;
            }
        }
        
        /// <remarks/>
        public GameRelease[] Releases {
            get {
                return this.releasesField;
            }
            set {
                this.releasesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.312")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://psp.ixtli.com/pspcompat/")]
    public partial class GameRelease {
        
        private long gameIDField;
        
        private long releaseIDField;
        
        private System.Nullable<int> regionField;
        
        private string discIDField;
        
        private string titleField;
        
        private string websiteField;
        
        private System.Nullable<System.DateTime> releaseDateField;
        
        private System.Nullable<float> gameVersionField;
        
        private System.Nullable<float> systemVersionField;
        
        private bool hasIconField;
        
        /// <remarks/>
        public long GameID {
            get {
                return this.gameIDField;
            }
            set {
                this.gameIDField = value;
            }
        }
        
        /// <remarks/>
        public long ReleaseID {
            get {
                return this.releaseIDField;
            }
            set {
                this.releaseIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<int> Region {
            get {
                return this.regionField;
            }
            set {
                this.regionField = value;
            }
        }
        
        /// <remarks/>
        public string DiscID {
            get {
                return this.discIDField;
            }
            set {
                this.discIDField = value;
            }
        }
        
        /// <remarks/>
        public string Title {
            get {
                return this.titleField;
            }
            set {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        public string Website {
            get {
                return this.websiteField;
            }
            set {
                this.websiteField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> ReleaseDate {
            get {
                return this.releaseDateField;
            }
            set {
                this.releaseDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<float> GameVersion {
            get {
                return this.gameVersionField;
            }
            set {
                this.gameVersionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<float> SystemVersion {
            get {
                return this.systemVersionField;
            }
            set {
                this.systemVersionField = value;
            }
        }
        
        /// <remarks/>
        public bool HasIcon {
            get {
                return this.hasIconField;
            }
            set {
                this.hasIconField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.312")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://psp.ixtli.com/pspcompat/")]
    public partial class CompatibilityResults {
        
        private bool foundField;
        
        private long gameIDField;
        
        private long releaseIDField;
        
        private Game gameField;
        
        private CompatibilityLevel levelField;
        
        private string[] notesField;
        
        /// <remarks/>
        public bool Found {
            get {
                return this.foundField;
            }
            set {
                this.foundField = value;
            }
        }
        
        /// <remarks/>
        public long GameID {
            get {
                return this.gameIDField;
            }
            set {
                this.gameIDField = value;
            }
        }
        
        /// <remarks/>
        public long ReleaseID {
            get {
                return this.releaseIDField;
            }
            set {
                this.releaseIDField = value;
            }
        }
        
        /// <remarks/>
        public Game Game {
            get {
                return this.gameField;
            }
            set {
                this.gameField = value;
            }
        }
        
        /// <remarks/>
        public CompatibilityLevel Level {
            get {
                return this.levelField;
            }
            set {
                this.levelField = value;
            }
        }
        
        /// <remarks/>
        public string[] Notes {
            get {
                return this.notesField;
            }
            set {
                this.notesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.312")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://psp.ixtli.com/pspcompat/")]
    public enum CompatibilityLevel {
        
        /// <remarks/>
        Unknown,
        
        /// <remarks/>
        Crashing,
        
        /// <remarks/>
        Menus,
        
        /// <remarks/>
        Playable,
        
        /// <remarks/>
        Complete,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.312")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://psp.ixtli.com/pspcompat/")]
    public partial class Version {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.312")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://psp.ixtli.com/pspcompat/")]
    public partial class ComponentInfo {
        
        private string filenameField;
        
        private ComponentInfoType typeField;
        
        private string nameField;
        
        private Version versionField;
        
        private string websiteField;
        
        private ComponentInfoBuild buildField;
        
        /// <remarks/>
        public string Filename {
            get {
                return this.filenameField;
            }
            set {
                this.filenameField = value;
            }
        }
        
        /// <remarks/>
        public ComponentInfoType Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public Version Version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
        
        /// <remarks/>
        public string Website {
            get {
                return this.websiteField;
            }
            set {
                this.websiteField = value;
            }
        }
        
        /// <remarks/>
        public ComponentInfoBuild Build {
            get {
                return this.buildField;
            }
            set {
                this.buildField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.312")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://psp.ixtli.com/pspcompat/")]
    public enum ComponentInfoType {
        
        /// <remarks/>
        Audio,
        
        /// <remarks/>
        Bios,
        
        /// <remarks/>
        Cpu,
        
        /// <remarks/>
        Input,
        
        /// <remarks/>
        UserMedia,
        
        /// <remarks/>
        GameMedia,
        
        /// <remarks/>
        Network,
        
        /// <remarks/>
        Video,
        
        /// <remarks/>
        Other,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.312")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://psp.ixtli.com/pspcompat/")]
    public enum ComponentInfoBuild {
        
        /// <remarks/>
        Debug,
        
        /// <remarks/>
        Testing,
        
        /// <remarks/>
        Release,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.312")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://psp.ixtli.com/pspcompat/")]
    public enum AddResult {
        
        /// <remarks/>
        Succeeded,
        
        /// <remarks/>
        PermissionDenied,
        
        /// <remarks/>
        Redundant,
        
        /// <remarks/>
        Failed,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.312")]
    public delegate void ListGamesCompletedEventHandler(object sender, ListGamesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.312")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ListGamesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ListGamesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Game[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Game[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.312")]
    public delegate void GetGameCompletedEventHandler(object sender, GetGameCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.312")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetGameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetGameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Game Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Game)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.312")]
    public delegate void AddGameCompletedEventHandler(object sender, AddGameCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.312")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddGameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddGameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public AddResult Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((AddResult)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.312")]
    public delegate void AddReleaseCompletedEventHandler(object sender, AddReleaseCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.312")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddReleaseCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddReleaseCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public AddResult Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((AddResult)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.312")]
    public delegate void CheckCompatibilityCompletedEventHandler(object sender, CheckCompatibilityCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.312")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CheckCompatibilityCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CheckCompatibilityCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public CompatibilityResults Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((CompatibilityResults)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591