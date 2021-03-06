﻿Imports System.ComponentModel
Namespace RuntimeT.CompilerServicesT


    ''' <summary>Abstract base class for post-processing attributes. This class defines common properties of postprocessing attributes.</summary>
    ''' <remarks>You typically want to apply <see cref="AttributeUsageAttribute"/> and <see cref="PostprocessorAttribute"/> attributes on class derived from this class.</remarks>
    ''' <seelaso cref="PostprocessorAttribute"/>
    ''' <seealso cref="T:Tools.RuntimeT.CompilerServicesT.AssemblyPostporcessor"/>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced), AttributeUsage(AttributeTargets.All, allowmultiple:=True, inherited:=False)>
    Public MustInherit Class PostprocessingAttribute
        Inherits Attribute
        ''' <summary>Gets or sets value indicating if attribute should be removed from member it decorates once postprocessing is done</summary>
        Public Property Remove As Boolean
        ''' <summary>Gets or sets value indicating if this attribute should be applied before members of item the attribute is applied on are processed</summary>
        ''' <remarks>AssemblyPostrocessor normally applies attributes first on child objects (e.g. methods of type) and then on type itself.</remarks>
        Public Property Preorder As Boolean
    End Class

End Namespace
