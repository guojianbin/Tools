﻿#pragma once

#using <mscorlib.dll>
#using <System.dll>

using namespace System::Security::Permissions;
[assembly:SecurityPermissionAttribute(SecurityAction::RequestMinimum, SkipVerification=false)];
namespace Tools {
    namespace TotalCommanderT {
        namespace ResourcesT {
    using namespace System;
    using namespace System;
    ref class Exceptions;
    
    
    /// <summary>
    /// A strongly-typed resource class, for looking up localized strings, formatting them, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilderEx class via the InternalResXFileCodeGeneratorEx custom tool.
    // To add or remove a member, edit your .ResX file then rerun the InternalResXFileCodeGeneratorEx custom tool or rebuild your VS.NET project.
    // Copyright © Dmytro Kryvko 2006-8 (http://dmytro.kryvko.googlepages.com/); Đonny 2008-2015 (http://tools.codeplex.com)
    [System::CodeDom::Compiler::GeneratedCodeAttribute(L"Tools.VisualStudioT.GeneratorsT.ResXFileGenerator.StronglyTypedResourceBuilderEx", 
    L"1.5.4.42058"), 
    System::Diagnostics::DebuggerNonUserCodeAttribute, 
    System::Diagnostics::CodeAnalysis::SuppressMessageAttribute(L"Microsoft.Naming", L"CA1724:TypeNamesShouldNotMatchNamespaces")]
    ref class Exceptions {
        
        private: static ::System::Resources::ResourceManager^  _resourceManager;
        
        private: static ::System::Object^  _internalSyncObject;
        
        private: static ::System::Globalization::CultureInfo^  _resourceCulture;
        
        private: [System::Diagnostics::CodeAnalysis::SuppressMessageAttribute(L"Microsoft.Performance", L"CA1811:AvoidUncalledPrivateCode")]
        Exceptions();
        /// <summary>
        /// Thread safe lock object used by this class.
        /// </summary>
        public: static property ::System::Object^  InternalSyncObject {
            ::System::Object^  get();
        }
        
        /// <summary>
        /// Returns the cached ResourceManager instance used by this class.
        /// </summary>
        public: [System::ComponentModel::EditorBrowsableAttribute(::System::ComponentModel::EditorBrowsableState::Advanced)]
        static property ::System::Resources::ResourceManager^  ResourceManager {
            ::System::Resources::ResourceManager^  get();
        }
        
        /// <summary>
        /// Overrides the current thread's CurrentUICulture property for all
        /// resource lookups using this strongly typed resource class.
        /// </summary>
        public: [System::ComponentModel::EditorBrowsableAttribute(::System::ComponentModel::EditorBrowsableState::Advanced)]
        static property ::System::Globalization::CultureInfo^  Culture {
            ::System::Globalization::CultureInfo^  get();
            System::Void set(::System::Globalization::CultureInfo^  value);
        }
        
        /// <summary>
        /// Looks up a localized string similar to '{0} cannot be represented as {1}, use {2} and {3} instead.'.
        /// </summary>
        public: static property System::String^  CannotBeRepresented {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Value of type {0} cannot be compared to {1}.'.
        /// </summary>
        public: static property System::String^  CannotCompare {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Column definition string returned by {0} is too long.'.
        /// </summary>
        public: static property System::String^  ColumnDefinitionStringTooLong {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Cryptography for this plugin has already been initialized.'.
        /// </summary>
        public: static property System::String^  CryptoAlreadyInitialized {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Crypto was not initialized'.
        /// </summary>
        public: static property System::String^  CryptoNotInitialized {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Default text is too long.'.
        /// </summary>
        public: static property System::String^  DefaultTextTooLong {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Method implementing or overriding {0}.{1} cannot be found on type {2}.'.
        /// </summary>
        public: static property System::String^  DerivedMehtodNotFound {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Encrypt/Decrypt failed'.
        /// </summary>
        public: static property System::String^  EncryptDecryptFailed {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to '{0} returned fields which&apos;s name is longer than {1}'.
        /// </summary>
        public: static property System::String^  FieldNameTooLong {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Generic parameters {0} not found on type {1}'.
        /// </summary>
        public: static property System::String^  GenericParameterNotFound {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Value &quot;{0}&quot; contains invalid character.'.
        /// </summary>
        public: static property System::String^  InvalidCharacter {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to '{0} returned field at index {1} that has FieldIndex set to {2}'.
        /// </summary>
        public: static property System::String^  InvalidFieldIndex {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Field name contain invalid character.'.
        /// </summary>
        public: static property System::String^  InvalidFieldNameCharacter {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to '{0:f} is not valid type of field.'.
        /// </summary>
        public: static property System::String^  InvalidFieldType {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Macor name &quot;{0}&quot; is invalid.'.
        /// </summary>
        public: static property System::String^  InvalidMacroName {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'The path {0} has invalid format.'.
        /// </summary>
        public: static property System::String^  InvalidPathFormat {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Attempt to reinitialize plugin with different number (old {0}, new {1})'.
        /// </summary>
        public: static property System::String^  InvalidPluginNumberReinitialization {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Unit name contains invalid character.'.
        /// </summary>
        public: static property System::String^  InvalidUnitNameCharacter {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Name too long. Mamximum allowed length is {0}'.
        /// </summary>
        public: static property System::String^  NameTooLong {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'No master password entered yet'.
        /// </summary>
        public: static property System::String^  NoMasterPassword {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'The class was not initialized'.
        /// </summary>
        public: static property System::String^  NotInitialized {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'The {0} parameter of the {1} method was assigned to long string.'.
        /// </summary>
        public: static property System::String^  ParamAssignedTooLong {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'String returned by plugin is longer than maximal path length alowed.'.
        /// </summary>
        public: static property System::String^  PathTooLong {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'The plugin was already initialized.'.
        /// </summary>
        public: static property System::String^  PluginInitialized {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Attempt to re-initialize plugin with different ANSI/Unicode settings.'.
        /// </summary>
        public: static property System::String^  PluginInitializedAnsiUnicode {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Attempt to re-initialize plugin in managed mode when it was already initialized in Total Commander mode.'.
        /// </summary>
        public: static property System::String^  PluginInitializedForTC {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Attempt to re-initialize plugin in Total Commander mode when it was alreay initialized in managed mode.'.
        /// </summary>
        public: static property System::String^  PluginInitializedNotForTC {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Plugin was not initialized.'.
        /// </summary>
        public: static property System::String^  PluginNotInitialized {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Plugin number {0} is not registered.'.
        /// </summary>
        public: static property System::String^  PluginNotRegistered {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Plugin type {0} is not supported'.
        /// </summary>
        public: static property System::String^  PluginTypeNotSupported {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Plugin window handle mismatch'.
        /// </summary>
        public: static property System::String^  PluginWindowHandleMismatch {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Property {0} is read-only on {1}'.
        /// </summary>
        public: static property System::String^  PropertyIsReadOnly {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'The {0} property have already been initialized.'.
        /// </summary>
        public: static property System::String^  PropertyWasInitialized {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'The {0} property have not been initialized yet.'.
        /// </summary>
        public: static property System::String^  PropertyWasNotInitialized {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to '{0} was null.'.
        /// </summary>
        public: static property System::String^  PropertyWasNull {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Password not found in password store'.
        /// </summary>
        public: static property System::String^  ReadPasswordStoreFailed {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Array returned by {0} is too long.'.
        /// </summary>
        public: static property System::String^  ReturnedArrayToLong {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'The string returned by {0} for the {1} type is too long.'.
        /// </summary>
        public: static property System::String^  ReturnedStringTooLongForChoice {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'To many {0} attributes specified on {1}.'.
        /// </summary>
        public: static property System::String^  ToManyAttributes {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Type {0} neither derives from nor implements {1}'.
        /// </summary>
        public: static property System::String^  TypeDoesNotDeriveFrom {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Type {0} is not generic'.
        /// </summary>
        public: static property System::String^  TypeIsNotGeneric {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Type must be specified for global methods'.
        /// </summary>
        public: static property System::String^  TypeMustBeSpecifiedForGlobalMethods {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Unexteced type {0} retunrned by {1}'.
        /// </summary>
        public: static property System::String^  UnexpectedType {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to '{0} returned unit names sum of which&apos;s length plus number of them minus 1 is more than {1}'.
        /// </summary>
        public: static property System::String^  UnitNamesTooLong {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Unknown error'.
        /// </summary>
        public: static property System::String^  UnknownError {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Value {0} is not supported'.
        /// </summary>
        public: static property System::String^  ValueNotSupported {
            System::String^  get();
        }
        
        /// <summary>
        /// Looks up a localized string similar to 'Could not write password to password store'.
        /// </summary>
        public: static property System::String^  WritePasswordStoreFailed {
            System::String^  get();
        }
        
        /// <summary>
        /// Formats a localized string similar to '{0} cannot be represented as {1}, use {2} and {3} instead.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <param name="arg1">An object (1) to format.</param>
        /// <param name="arg2">An object (2) to format.</param>
        /// <param name="arg3">An object (3) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: [System::Diagnostics::CodeAnalysis::SuppressMessageAttribute(L"Microsoft.Design", L"CA1025:ReplaceRepetitiveArgumentsWithParamsArray")]
        static System::String^  CannotBeRepresentedFormat(System::Object^  arg0, System::Object^  arg1, System::Object^  arg2, 
                    System::Object^  arg3);
        
        /// <summary>
        /// Formats a localized string similar to 'Value of type {0} cannot be compared to {1}.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <param name="arg1">An object (1) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  CannotCompareFormat(System::Object^  arg0, System::Object^  arg1);
        
        /// <summary>
        /// Formats a localized string similar to 'Column definition string returned by {0} is too long.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  ColumnDefinitionStringTooLongFormat(System::Object^  arg0);
        
        /// <summary>
        /// The stub formatting method returning the CryptoAlreadyInitialized property value.
        /// </summary>
        /// <returns>The CryptoAlreadyInitialized property value.</returns>
        public: static System::String^  CryptoAlreadyInitializedFormat();
        
        /// <summary>
        /// The stub formatting method returning the CryptoNotInitialized property value.
        /// </summary>
        /// <returns>The CryptoNotInitialized property value.</returns>
        public: static System::String^  CryptoNotInitializedFormat();
        
        /// <summary>
        /// The stub formatting method returning the DefaultTextTooLong property value.
        /// </summary>
        /// <returns>The DefaultTextTooLong property value.</returns>
        public: static System::String^  DefaultTextTooLongFormat();
        
        /// <summary>
        /// Formats a localized string similar to 'Method implementing or overriding {0}.{1} cannot be found on type {2}.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <param name="arg1">An object (1) to format.</param>
        /// <param name="arg2">An object (2) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  DerivedMehtodNotFoundFormat(System::Object^  arg0, System::Object^  arg1, System::Object^  arg2);
        
        /// <summary>
        /// The stub formatting method returning the EncryptDecryptFailed property value.
        /// </summary>
        /// <returns>The EncryptDecryptFailed property value.</returns>
        public: static System::String^  EncryptDecryptFailedFormat();
        
        /// <summary>
        /// Formats a localized string similar to '{0} returned fields which&apos;s name is longer than {1}'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <param name="arg1">An object (1) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  FieldNameTooLongFormat(System::Object^  arg0, System::Object^  arg1);
        
        /// <summary>
        /// Formats a localized string similar to 'Generic parameters {0} not found on type {1}'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <param name="arg1">An object (1) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  GenericParameterNotFoundFormat(System::Object^  arg0, System::Object^  arg1);
        
        /// <summary>
        /// Formats a localized string similar to 'Value &quot;{0}&quot; contains invalid character.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  InvalidCharacterFormat(System::Object^  arg0);
        
        /// <summary>
        /// Formats a localized string similar to '{0} returned field at index {1} that has FieldIndex set to {2}'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <param name="arg1">An object (1) to format.</param>
        /// <param name="arg2">An object (2) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  InvalidFieldIndexFormat(System::Object^  arg0, System::Object^  arg1, System::Object^  arg2);
        
        /// <summary>
        /// The stub formatting method returning the InvalidFieldNameCharacter property value.
        /// </summary>
        /// <returns>The InvalidFieldNameCharacter property value.</returns>
        public: static System::String^  InvalidFieldNameCharacterFormat();
        
        /// <summary>
        /// Formats a localized string similar to '{0:f} is not valid type of field.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  InvalidFieldTypeFormat(System::Object^  arg0);
        
        /// <summary>
        /// Formats a localized string similar to 'Macor name &quot;{0}&quot; is invalid.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  InvalidMacroNameFormat(System::Object^  arg0);
        
        /// <summary>
        /// Formats a localized string similar to 'The path {0} has invalid format.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  InvalidPathFormatFormat(System::Object^  arg0);
        
        /// <summary>
        /// Formats a localized string similar to 'Attempt to reinitialize plugin with different number (old {0}, new {1})'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <param name="arg1">An object (1) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  InvalidPluginNumberReinitializationFormat(System::Object^  arg0, System::Object^  arg1);
        
        /// <summary>
        /// The stub formatting method returning the InvalidUnitNameCharacter property value.
        /// </summary>
        /// <returns>The InvalidUnitNameCharacter property value.</returns>
        public: static System::String^  InvalidUnitNameCharacterFormat();
        
        /// <summary>
        /// Formats a localized string similar to 'Name too long. Mamximum allowed length is {0}'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  NameTooLongFormat(System::Object^  arg0);
        
        /// <summary>
        /// The stub formatting method returning the NoMasterPassword property value.
        /// </summary>
        /// <returns>The NoMasterPassword property value.</returns>
        public: static System::String^  NoMasterPasswordFormat();
        
        /// <summary>
        /// The stub formatting method returning the NotInitialized property value.
        /// </summary>
        /// <returns>The NotInitialized property value.</returns>
        public: static System::String^  NotInitializedFormat();
        
        /// <summary>
        /// Formats a localized string similar to 'The {0} parameter of the {1} method was assigned to long string.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <param name="arg1">An object (1) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  ParamAssignedTooLongFormat(System::Object^  arg0, System::Object^  arg1);
        
        /// <summary>
        /// The stub formatting method returning the PathTooLong property value.
        /// </summary>
        /// <returns>The PathTooLong property value.</returns>
        public: static System::String^  PathTooLongFormat();
        
        /// <summary>
        /// The stub formatting method returning the PluginInitialized property value.
        /// </summary>
        /// <returns>The PluginInitialized property value.</returns>
        public: static System::String^  PluginInitializedFormat();
        
        /// <summary>
        /// The stub formatting method returning the PluginInitializedAnsiUnicode property value.
        /// </summary>
        /// <returns>The PluginInitializedAnsiUnicode property value.</returns>
        public: static System::String^  PluginInitializedAnsiUnicodeFormat();
        
        /// <summary>
        /// The stub formatting method returning the PluginInitializedForTC property value.
        /// </summary>
        /// <returns>The PluginInitializedForTC property value.</returns>
        public: static System::String^  PluginInitializedForTCFormat();
        
        /// <summary>
        /// The stub formatting method returning the PluginInitializedNotForTC property value.
        /// </summary>
        /// <returns>The PluginInitializedNotForTC property value.</returns>
        public: static System::String^  PluginInitializedNotForTCFormat();
        
        /// <summary>
        /// The stub formatting method returning the PluginNotInitialized property value.
        /// </summary>
        /// <returns>The PluginNotInitialized property value.</returns>
        public: static System::String^  PluginNotInitializedFormat();
        
        /// <summary>
        /// Formats a localized string similar to 'Plugin number {0} is not registered.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  PluginNotRegisteredFormat(System::Object^  arg0);
        
        /// <summary>
        /// Formats a localized string similar to 'Plugin type {0} is not supported'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  PluginTypeNotSupportedFormat(System::Object^  arg0);
        
        /// <summary>
        /// The stub formatting method returning the PluginWindowHandleMismatch property value.
        /// </summary>
        /// <returns>The PluginWindowHandleMismatch property value.</returns>
        public: static System::String^  PluginWindowHandleMismatchFormat();
        
        /// <summary>
        /// Formats a localized string similar to 'Property {0} is read-only on {1}'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <param name="arg1">An object (1) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  PropertyIsReadOnlyFormat(System::Object^  arg0, System::Object^  arg1);
        
        /// <summary>
        /// Formats a localized string similar to 'The {0} property have already been initialized.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  PropertyWasInitializedFormat(System::Object^  arg0);
        
        /// <summary>
        /// Formats a localized string similar to 'The {0} property have not been initialized yet.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  PropertyWasNotInitializedFormat(System::Object^  arg0);
        
        /// <summary>
        /// Formats a localized string similar to '{0} was null.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  PropertyWasNullFormat(System::Object^  arg0);
        
        /// <summary>
        /// The stub formatting method returning the ReadPasswordStoreFailed property value.
        /// </summary>
        /// <returns>The ReadPasswordStoreFailed property value.</returns>
        public: static System::String^  ReadPasswordStoreFailedFormat();
        
        /// <summary>
        /// Formats a localized string similar to 'Array returned by {0} is too long.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  ReturnedArrayToLongFormat(System::Object^  arg0);
        
        /// <summary>
        /// Formats a localized string similar to 'The string returned by {0} for the {1} type is too long.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <param name="arg1">An object (1) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  ReturnedStringTooLongForChoiceFormat(System::Object^  arg0, System::Object^  arg1);
        
        /// <summary>
        /// Formats a localized string similar to 'To many {0} attributes specified on {1}.'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <param name="arg1">An object (1) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  ToManyAttributesFormat(System::Object^  arg0, System::Object^  arg1);
        
        /// <summary>
        /// Formats a localized string similar to 'Type {0} neither derives from nor implements {1}'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <param name="arg1">An object (1) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  TypeDoesNotDeriveFromFormat(System::Object^  arg0, System::Object^  arg1);
        
        /// <summary>
        /// Formats a localized string similar to 'Type {0} is not generic'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  TypeIsNotGenericFormat(System::Object^  arg0);
        
        /// <summary>
        /// The stub formatting method returning the TypeMustBeSpecifiedForGlobalMethods property value.
        /// </summary>
        /// <returns>The TypeMustBeSpecifiedForGlobalMethods property value.</returns>
        public: static System::String^  TypeMustBeSpecifiedForGlobalMethodsFormat();
        
        /// <summary>
        /// Formats a localized string similar to 'Unexteced type {0} retunrned by {1}'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <param name="arg1">An object (1) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  UnexpectedTypeFormat(System::Object^  arg0, System::Object^  arg1);
        
        /// <summary>
        /// Formats a localized string similar to '{0} returned unit names sum of which&apos;s length plus number of them minus 1 is more than {1}'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <param name="arg1">An object (1) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  UnitNamesTooLongFormat(System::Object^  arg0, System::Object^  arg1);
        
        /// <summary>
        /// The stub formatting method returning the UnknownError property value.
        /// </summary>
        /// <returns>The UnknownError property value.</returns>
        public: static System::String^  UnknownErrorFormat();
        
        /// <summary>
        /// Formats a localized string similar to 'Value {0} is not supported'.
        /// </summary>
        /// <param name="arg0">An object (0) to format.</param>
        /// <returns>A copy of format string in which the format items have been replaced by the String equivalent of the corresponding instances of Object in arguments.</returns>
        public: static System::String^  ValueNotSupportedFormat(System::Object^  arg0);
        
        /// <summary>
        /// The stub formatting method returning the WritePasswordStoreFailed property value.
        /// </summary>
        /// <returns>The WritePasswordStoreFailed property value.</returns>
        public: static System::String^  WritePasswordStoreFailedFormat();
    };
        }
    }
}