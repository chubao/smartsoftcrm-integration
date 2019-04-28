﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartlifeCRMIntegration.CallControllerService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CallControllerService.IServiceController", CallbackContract=typeof(SmartlifeCRMIntegration.CallControllerService.IServiceControllerCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IServiceController {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/InitailState", ReplyAction="http://tempuri.org/IServiceController/InitailStateResponse")]
        string InitailState(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/InitailState", ReplyAction="http://tempuri.org/IServiceController/InitailStateResponse")]
        System.Threading.Tasks.Task<string> InitailStateAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/GetInitailDN", ReplyAction="http://tempuri.org/IServiceController/GetInitailDNResponse")]
        string GetInitailDN();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/GetInitailDN", ReplyAction="http://tempuri.org/IServiceController/GetInitailDNResponse")]
        System.Threading.Tasks.Task<string> GetInitailDNAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/BargeinCall", ReplyAction="http://tempuri.org/IServiceController/BargeinCallResponse")]
        void BargeinCall(int callID, string dn, bool oneWayAudio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/BargeinCall", ReplyAction="http://tempuri.org/IServiceController/BargeinCallResponse")]
        System.Threading.Tasks.Task BargeinCallAsync(int callID, string dn, bool oneWayAudio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/DropCall", ReplyAction="http://tempuri.org/IServiceController/DropCallResponse")]
        void DropCall(int callID, string dn);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/DropCall", ReplyAction="http://tempuri.org/IServiceController/DropCallResponse")]
        System.Threading.Tasks.Task DropCallAsync(int callID, string dn);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/DivertCall", ReplyAction="http://tempuri.org/IServiceController/DivertCallResponse")]
        void DivertCall(int CallID, string dn, string destination, bool VMBox);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/DivertCall", ReplyAction="http://tempuri.org/IServiceController/DivertCallResponse")]
        System.Threading.Tasks.Task DivertCallAsync(int CallID, string dn, string destination, bool VMBox);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/Listen", ReplyAction="http://tempuri.org/IServiceController/ListenResponse")]
        void Listen(int CallID, string dn, string listen_by);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/Listen", ReplyAction="http://tempuri.org/IServiceController/ListenResponse")]
        System.Threading.Tasks.Task ListenAsync(int CallID, string dn, string listen_by);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/MakeCall", ReplyAction="http://tempuri.org/IServiceController/MakeCallResponse")]
        void MakeCall(string dn_from, string number_to);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/MakeCall", ReplyAction="http://tempuri.org/IServiceController/MakeCallResponse")]
        System.Threading.Tasks.Task MakeCallAsync(string dn_from, string number_to);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/PickupCall", ReplyAction="http://tempuri.org/IServiceController/PickupCallResponse")]
        void PickupCall(string dn, string dnFrom);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/PickupCall", ReplyAction="http://tempuri.org/IServiceController/PickupCallResponse")]
        System.Threading.Tasks.Task PickupCallAsync(string dn, string dnFrom);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/TransferCall", ReplyAction="http://tempuri.org/IServiceController/TransferCallResponse")]
        void TransferCall(int CallID, string dn, string number_to);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/TransferCall", ReplyAction="http://tempuri.org/IServiceController/TransferCallResponse")]
        System.Threading.Tasks.Task TransferCallAsync(int CallID, string dn, string number_to);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/WhisperTo", ReplyAction="http://tempuri.org/IServiceController/WhisperToResponse")]
        void WhisperTo(int CallID, string dn, string whisper_by);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/WhisperTo", ReplyAction="http://tempuri.org/IServiceController/WhisperToResponse")]
        System.Threading.Tasks.Task WhisperToAsync(int CallID, string dn, string whisper_by);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/Ping", ReplyAction="http://tempuri.org/IServiceController/PingResponse")]
        bool Ping();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/Ping", ReplyAction="http://tempuri.org/IServiceController/PingResponse")]
        System.Threading.Tasks.Task<bool> PingAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceControllerCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/Inserted", ReplyAction="http://tempuri.org/IServiceController/InsertedResponse")]
        void Inserted(string Message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/Updated", ReplyAction="http://tempuri.org/IServiceController/UpdatedResponse")]
        void Updated(string Message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceController/Deleted", ReplyAction="http://tempuri.org/IServiceController/DeletedResponse")]
        void Deleted(string Message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceControllerChannel : SmartlifeCRMIntegration.CallControllerService.IServiceController, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceControllerClient : System.ServiceModel.DuplexClientBase<SmartlifeCRMIntegration.CallControllerService.IServiceController>, SmartlifeCRMIntegration.CallControllerService.IServiceController {
        
        public ServiceControllerClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ServiceControllerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ServiceControllerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceControllerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceControllerClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public string InitailState(string name) {
            return base.Channel.InitailState(name);
        }
        
        public System.Threading.Tasks.Task<string> InitailStateAsync(string name) {
            return base.Channel.InitailStateAsync(name);
        }
        
        public string GetInitailDN() {
            return base.Channel.GetInitailDN();
        }
        
        public System.Threading.Tasks.Task<string> GetInitailDNAsync() {
            return base.Channel.GetInitailDNAsync();
        }
        
        public void BargeinCall(int callID, string dn, bool oneWayAudio) {
            base.Channel.BargeinCall(callID, dn, oneWayAudio);
        }
        
        public System.Threading.Tasks.Task BargeinCallAsync(int callID, string dn, bool oneWayAudio) {
            return base.Channel.BargeinCallAsync(callID, dn, oneWayAudio);
        }
        
        public void DropCall(int callID, string dn) {
            base.Channel.DropCall(callID, dn);
        }
        
        public System.Threading.Tasks.Task DropCallAsync(int callID, string dn) {
            return base.Channel.DropCallAsync(callID, dn);
        }
        
        public void DivertCall(int CallID, string dn, string destination, bool VMBox) {
            base.Channel.DivertCall(CallID, dn, destination, VMBox);
        }
        
        public System.Threading.Tasks.Task DivertCallAsync(int CallID, string dn, string destination, bool VMBox) {
            return base.Channel.DivertCallAsync(CallID, dn, destination, VMBox);
        }
        
        public void Listen(int CallID, string dn, string listen_by) {
            base.Channel.Listen(CallID, dn, listen_by);
        }
        
        public System.Threading.Tasks.Task ListenAsync(int CallID, string dn, string listen_by) {
            return base.Channel.ListenAsync(CallID, dn, listen_by);
        }
        
        public void MakeCall(string dn_from, string number_to) {
            base.Channel.MakeCall(dn_from, number_to);
        }
        
        public System.Threading.Tasks.Task MakeCallAsync(string dn_from, string number_to) {
            return base.Channel.MakeCallAsync(dn_from, number_to);
        }
        
        public void PickupCall(string dn, string dnFrom) {
            base.Channel.PickupCall(dn, dnFrom);
        }
        
        public System.Threading.Tasks.Task PickupCallAsync(string dn, string dnFrom) {
            return base.Channel.PickupCallAsync(dn, dnFrom);
        }
        
        public void TransferCall(int CallID, string dn, string number_to) {
            base.Channel.TransferCall(CallID, dn, number_to);
        }
        
        public System.Threading.Tasks.Task TransferCallAsync(int CallID, string dn, string number_to) {
            return base.Channel.TransferCallAsync(CallID, dn, number_to);
        }
        
        public void WhisperTo(int CallID, string dn, string whisper_by) {
            base.Channel.WhisperTo(CallID, dn, whisper_by);
        }
        
        public System.Threading.Tasks.Task WhisperToAsync(int CallID, string dn, string whisper_by) {
            return base.Channel.WhisperToAsync(CallID, dn, whisper_by);
        }
        
        public bool Ping() {
            return base.Channel.Ping();
        }
        
        public System.Threading.Tasks.Task<bool> PingAsync() {
            return base.Channel.PingAsync();
        }
    }
}
