﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30128.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tools.ResourcesT {
    using System;
    
    
    /// <summary>
    /// A strongly-typed resource class, for looking up localized strings, formatting them, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilderEx class via the InternalResXFileCodeGeneratorEx custom tool.
    // To add or remove a member, edit your .ResX file then rerun the InternalResXFileCodeGeneratorEx custom tool or rebuild your VS.NET project.
    // Copyright (c) Dmytro Kryvko 2006-2010 (http://dmytro.kryvko.googlepages.com/)
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("DMKSoftware.CodeGenerators.Tools.StronglyTypedResourceBuilderEx", "2.2.5.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
    internal class ExceptionsVsCs {
        
        private static global::System.Resources.ResourceManager _resourceManager;
        
        private static object _internalSyncObject;
        
        private static global::System.Globalization.CultureInfo _resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ExceptionsVsCs() {
        }
        
        /// <summary>
        /// Thread safe lock object used by this class.
        /// </summary>
        internal static object InternalSyncObject {
            get {
                if (object.ReferenceEquals(_internalSyncObject, null)) {
                    global::System.Threading.Interlocked.CompareExchange(ref _internalSyncObject, new object(), null);
                }
                return _internalSyncObject;
            }
        }
        
        /// <summary>
        /// Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(_resourceManager, null)) {
                    global::System.Threading.Monitor.Enter(InternalSyncObject);
                    try {
                        if (object.ReferenceEquals(_resourceManager, null)) {
                            global::System.Threading.Interlocked.Exchange(ref _resourceManager, new global::System.Resources.ResourceManager("Tools.ResourcesT.ExceptionsVsCs", typeof(ExceptionsVsCs).Assembly));
                        }
                    }
                    finally {
                        global::System.Threading.Monitor.Exit(InternalSyncObject);
                    }
                }
                return _resourceManager;
            }
        }
        
        /// <summary>
        /// Overrides the current thread's CurrentUICulture property for all
        /// resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return _resourceCulture;
            }
            set {
                _resourceCulture = value;
            }
        }
        
        /// <summary>
        /// Looks up a localized string similar to '{0} array must have at least 1 member'.
        /// </summary>
        internal static string ArrayMustHaveAtLeast1Member {
            get {
                return ResourceManager.GetString("ArrayMustHaveAtLeast1Member", _resourceCulture);
            }
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Class &apos;{0}&apos; does not provide a &apos;{1}&apos; attribute.'.
        /// </summary>
        internal static string Class0DoesNotProvideA1Attribute {
            get {
                return ResourceManager.GetString("Class0DoesNotProvideA1Attribute", _resourceCulture);
            }
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'CodeDomProvider is NULL.'.
        /// </summary>
        internal static string CodeDomProviderIsNULL {
            get {
                return ResourceManager.GetString("CodeDomProviderIsNULL", _resourceCulture);
            }
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'CustomToolClass must have {0} applied.'.
        /// </summary>
        internal static string CustomToolClassMustHave0Applied {
            get {
                return ResourceManager.GetString("CustomToolClassMustHave0Applied", _resourceCulture);
            }
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Custom tool must inherit from CustomTollBase.'.
        /// </summary>
        internal static string CustomToolMustInheritFromCustomToolBase {
            get {
                return ResourceManager.GetString("CustomToolMustInheritFromCustomToolBase", _resourceCulture);
            }
        }
        
        /// <summary>
        /// Looks up a localized string similar to '**** ERROR: vsProj.References.Add() throws exception: '.
        /// </summary>
        internal static string ERRORVsProjReferencesAddThrowsException {
            get {
                return ResourceManager.GetString("ERRORVsProjReferencesAddThrowsException", _resourceCulture);
            }
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Get CodeDomProvider Interface failed.  GetService( QueryService( CodeDomProvider ) returned Null.'.
        /// </summary>
        internal static string GetCodeDomProviderInterfaceFailedGetServiceQueryServiceCodeDomProviderReturnedNull {
            get {
                return ResourceManager.GetString("GetCodeDomProviderInterfaceFailedGetServiceQueryServiceCodeDomProviderReturnedNul" +
                        "l", _resourceCulture);
            }
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'GetService( typeof( Project ) ) return null.'.
        /// </summary>
        internal static string GetServiceTypeofProjectReturnNull {
            get {
                return ResourceManager.GetString("GetServiceTypeofProjectReturnNull", _resourceCulture);
            }
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Guid cannot be empty.'.
        /// </summary>
        internal static string GuidCannotBeEmpty {
            get {
                return ResourceManager.GetString("GuidCannotBeEmpty", _resourceCulture);
            }
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'object is not sited'.
        /// </summary>
        internal static string ObjectIsNotSited {
            get {
                return ResourceManager.GetString("ObjectIsNotSited", _resourceCulture);
            }
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'site does not support requested interface'.
        /// </summary>
        internal static string SiteDoesNotSupportRequestedInterface {
            get {
                return ResourceManager.GetString("SiteDoesNotSupportRequestedInterface", _resourceCulture);
            }
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Unable to ADD DLL to current project.  Project.Object does not implement VSProject.'.
        /// </summary>
        internal static string UnableToADDDLLToCurrentProjectProjectObjectDoesNotImplementVSProject {
            get {
                return ResourceManager.GetString("UnableToADDDLLToCurrentProjectProjectObjectDoesNotImplementVSProject", _resourceCulture);
            }
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Unable to add DLL to project references: {0}.  Please Add them manually.'.
        /// </summary>
        internal static string UnableToAddDLLToProjectReferences0PleaseAddThemManually {
            get {
                return ResourceManager.GetString("UnableToAddDLLToProjectReferences0PleaseAddThemManually", _resourceCulture);
            }
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Unable to get IOleServiceProvider from site object.'.
        /// </summary>
        internal static string UnableToGetIOleServiceProviderFromSiteObject {
            get {
                return ResourceManager.GetString("UnableToGetIOleServiceProviderFromSiteObject", _resourceCulture);
            }
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Unable to get Project Item.'.
        /// </summary>
        internal static string UnableToGetProjectItem {
            get {
                return ResourceManager.GetString("UnableToGetProjectItem", _resourceCulture);
            }
        }
        
        /// <summary>
        /// Formats a localized string similar to '{0} array must have at least 1 member'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        internal static string ArrayMustHaveAtLeast1MemberFormat(object arg0) {
            return string.Format(_resourceCulture, ArrayMustHaveAtLeast1Member, arg0);
        }
        
        /// <summary>
        /// Formats a localized string similar to 'Class &apos;{0}&apos; does not provide a &apos;{1}&apos; attribute.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <param name="arg1">An object (1) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        internal static string Class0DoesNotProvideA1AttributeFormat(object arg0, object arg1) {
            return string.Format(_resourceCulture, Class0DoesNotProvideA1Attribute, arg0, arg1);
        }
        
        /// <summary>
        /// The stub formatting method returning the CodeDomProviderIsNULL property value.
        /// </summary>
        /// <returns>The CodeDomProviderIsNULL property value.</returns>
        internal static string CodeDomProviderIsNULLFormat() {
            return CodeDomProviderIsNULL;
        }
        
        /// <summary>
        /// Formats a localized string similar to 'CustomToolClass must have {0} applied.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        internal static string CustomToolClassMustHave0AppliedFormat(object arg0) {
            return string.Format(_resourceCulture, CustomToolClassMustHave0Applied, arg0);
        }
        
        /// <summary>
        /// The stub formatting method returning the CustomToolMustInheritFromCustomToolBase property value.
        /// </summary>
        /// <returns>The CustomToolMustInheritFromCustomToolBase property value.</returns>
        internal static string CustomToolMustInheritFromCustomToolBaseFormat() {
            return CustomToolMustInheritFromCustomToolBase;
        }
        
        /// <summary>
        /// The stub formatting method returning the ERRORVsProjReferencesAddThrowsException property value.
        /// </summary>
        /// <returns>The ERRORVsProjReferencesAddThrowsException property value.</returns>
        internal static string ERRORVsProjReferencesAddThrowsExceptionFormat() {
            return ERRORVsProjReferencesAddThrowsException;
        }
        
        /// <summary>
        /// The stub formatting method returning the GetCodeDomProviderInterfaceFailedGetServiceQueryServiceCodeDomProviderReturnedNull property value.
        /// </summary>
        /// <returns>The GetCodeDomProviderInterfaceFailedGetServiceQueryServiceCodeDomProviderReturnedNull property value.</returns>
        internal static string GetCodeDomProviderInterfaceFailedGetServiceQueryServiceCodeDomProviderReturnedNullFormat() {
            return GetCodeDomProviderInterfaceFailedGetServiceQueryServiceCodeDomProviderReturnedNull;
        }
        
        /// <summary>
        /// The stub formatting method returning the GetServiceTypeofProjectReturnNull property value.
        /// </summary>
        /// <returns>The GetServiceTypeofProjectReturnNull property value.</returns>
        internal static string GetServiceTypeofProjectReturnNullFormat() {
            return GetServiceTypeofProjectReturnNull;
        }
        
        /// <summary>
        /// The stub formatting method returning the GuidCannotBeEmpty property value.
        /// </summary>
        /// <returns>The GuidCannotBeEmpty property value.</returns>
        internal static string GuidCannotBeEmptyFormat() {
            return GuidCannotBeEmpty;
        }
        
        /// <summary>
        /// The stub formatting method returning the ObjectIsNotSited property value.
        /// </summary>
        /// <returns>The ObjectIsNotSited property value.</returns>
        internal static string ObjectIsNotSitedFormat() {
            return ObjectIsNotSited;
        }
        
        /// <summary>
        /// The stub formatting method returning the SiteDoesNotSupportRequestedInterface property value.
        /// </summary>
        /// <returns>The SiteDoesNotSupportRequestedInterface property value.</returns>
        internal static string SiteDoesNotSupportRequestedInterfaceFormat() {
            return SiteDoesNotSupportRequestedInterface;
        }
        
        /// <summary>
        /// The stub formatting method returning the UnableToADDDLLToCurrentProjectProjectObjectDoesNotImplementVSProject property value.
        /// </summary>
        /// <returns>The UnableToADDDLLToCurrentProjectProjectObjectDoesNotImplementVSProject property value.</returns>
        internal static string UnableToADDDLLToCurrentProjectProjectObjectDoesNotImplementVSProjectFormat() {
            return UnableToADDDLLToCurrentProjectProjectObjectDoesNotImplementVSProject;
        }
        
        /// <summary>
        /// Formats a localized string similar to 'Unable to add DLL to project references: {0}.  Please Add them manually.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        internal static string UnableToAddDLLToProjectReferences0PleaseAddThemManuallyFormat(object arg0) {
            return string.Format(_resourceCulture, UnableToAddDLLToProjectReferences0PleaseAddThemManually, arg0);
        }
        
        /// <summary>
        /// The stub formatting method returning the UnableToGetIOleServiceProviderFromSiteObject property value.
        /// </summary>
        /// <returns>The UnableToGetIOleServiceProviderFromSiteObject property value.</returns>
        internal static string UnableToGetIOleServiceProviderFromSiteObjectFormat() {
            return UnableToGetIOleServiceProviderFromSiteObject;
        }
        
        /// <summary>
        /// The stub formatting method returning the UnableToGetProjectItem property value.
        /// </summary>
        /// <returns>The UnableToGetProjectItem property value.</returns>
        internal static string UnableToGetProjectItemFormat() {
            return UnableToGetProjectItem;
        }
    }
}
