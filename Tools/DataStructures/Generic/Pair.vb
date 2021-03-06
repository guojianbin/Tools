﻿Namespace DataStructuresT.GenericT
#If True
    ''' <summary>Type tha contains value of two distinct types</summary>
    ''' <typeparam name="T1">Type of first value</typeparam>
    ''' <typeparam name="T2">Type of second value</typeparam>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Interface IPair(Of T1, T2) : Inherits ICloneable(Of IPair(Of T1, T2))
        ''' <summary>Value of type <see cref="T1"/></summary>
        Property Value1() As T1
        ''' <summary>Value of type <typeparamref name="T2"/></summary>
        Property Value2() As T2
        ''' <summary>Swaps values <see cref="Value1"/> and <see cref="Value2"/></summary>
        Function Swap() As IPair(Of T2, T1)
    End Interface
#End If

#If True
    ''' <summary>Implements <see cref="IPair(Of T1,T2)"/> as reference type</summary>
    ''' <typeparam name="T1">Type of Value1</typeparam>
    ''' <typeparam name="T2">Type of Value2</typeparam>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    ''' <version version="1.5.4">THis class now overrides methods <see cref="[Object].Equals"/> and <see cref="[Object].GetHashCode"/> and <see cref="[Object].ToString"/>.</version>
    Public Class Pair(Of T1, T2)
        Inherits Cloenable(Of Pair(Of T1, T2))
        Implements IPair(Of T1, T2)
        ''' <summary>Contains value of the <see cref="Value1"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Value1 As T1
        ''' <summary>Contains value of the <see cref="Value2"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Value2 As T2
        ''' <summary>CTor - initialize with two values</summary>
        ''' <param name="V1">Initial value for <see cref="Value1"/></param>
        ''' <param name="V2">Initial value for <see cref="Value2"/></param>
        Public Sub New(ByVal V1 As T1, ByVal V2 As T2)
            Value1 = V1
            Value2 = V2
        End Sub
        ''' <summary>CTor - initialize with another instance of <see cref="IPair(Of T1, T2)"/></summary>
        ''' <param name="a">Instance to initialize new istance</param>
        Public Sub New(ByVal a As IPair(Of T1, T2))
            Me.New(a.Value1, a.Value2)
        End Sub
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Function Clone1() As IPair(Of T1, T2) Implements ICloneable(Of IPair(Of T1, T2)).Clone
            Return Clone()
        End Function

        ''' <summary>Swaps values <see cref="Value1"/> and <see cref="Value2"/></summary>        
        Public Function Swap() As IPair(Of T2, T1) Implements IPair(Of T1, T2).Swap
            Return New Pair(Of T2, T1)(Value2, Value1)
        End Function

        ''' <summary>Value of type <see cref="T1"/></summary>        
        Public Overridable Property Value1() As T1 Implements IPair(Of T1, T2).Value1
            Get
                Return _Value1
            End Get
            Set(ByVal value As T1)
                _Value1 = value
            End Set
        End Property

        ''' <summary>Value of type <typeparamref name="T2"/></summary>        
        Public Overridable Property Value2() As T2 Implements IPair(Of T1, T2).Value2
            Get
                Return _Value2
            End Get
            Set(ByVal value As T2)
                _Value2 = value
            End Set
        End Property

        ''' <summary>Returns new instance of <see cref="IPair(Of T1, T2)"/> initialized with current instance</summary>
        ''' <returns>New instance of <see cref="IPair(Of T1, T2)"/> initialized with current instance if either <see cref="T1"/> or <typeparamref name="T2"/> implements <see cref="ICloneable"/> then they are also cloned via <see cref="ICloneable.Clone"/></returns>
        Public Overrides Function Clone() As Pair(Of T1, T2)
            Dim new1 As T1, new2 As T2
            If TypeOf Value1 Is ICloneable Then
                new1 = CType(Value1, ICloneable).Clone
            Else
                new1 = Value1
            End If
            If TypeOf Value2 Is ICloneable Then
                new2 = CType(Value2, ICloneable).Clone
            Else
                new2 = Value2
            End If
            Return New Pair(Of T1, T2)(new1, new2)
        End Function

        ''' <summary>Converts <see cref="Pair(Of T1, T2)"/> into <see cref="KeyValuePair(Of T1, T2)"/></summary>
        ''' <param name="a">Value to be converted</param>
        ''' <returns>Converted <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As Pair(Of T1, T2)) As KeyValuePair(Of T1, T2)
            Return New KeyValuePair(Of T1, T2)(a.Value1, a.Value2)
        End Operator
        ''' <summary>Converts <see cref="KeyValuePair(Of T1, T2)"/> into <see cref="Pair(Of T1, T2)"/></summary>
        ''' <param name="a">Value to be converted</param>
        ''' <returns>Converted <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As KeyValuePair(Of T1, T2)) As Pair(Of T1, T2)
            Return New Pair(Of T1, T2)(a.Key, a.Value)
        End Operator

        ''' <summary>Serves as a hash function for a particular type. </summary>
        ''' <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
        ''' <version>This method override at this level is new in version 1.5.4</version>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function GetHashCode() As Integer
            Return If(Value1 Is Nothing, 0, Value1.GetHashCode) Or If(Value2 Is Nothing, 0, Value2.GetHashCode)
        End Function
        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        ''' <version>This method override at this level is new in version 1.5.4</version>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function Equals(obj As Object) As Boolean
            If Not TypeOf obj Is Pair(Of T1, T2) Then Return MyBase.Equals(obj)
            Dim o As Pair(Of T1, T2) = obj
            Return ((Me.Value1 Is Nothing) = (o.Value1 Is Nothing) AndAlso (Me.Value1 Is Nothing OrElse Me.Value1.Equals(o.Value1))) AndAlso
                   ((Me.Value2 Is Nothing) = (o.Value2 Is Nothing) AndAlso (Me.Value2 Is Nothing OrElse Me.Value2.Equals(o.Value2)))
        End Function

        ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
        ''' <returns>
        ''' A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
        ''' This implementation returns string values of <see cref="Value1"/> and <see cref="Value2"/> in format <c>{{Value1}, {Value2}}</c>.
        ''' </returns>
        ''' <version>This method override at this level is new in version 1.5.4</version>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function ToString() As String
            Return String.Format("{{{0}, {1}}}", Value1, Value2)
        End Function
    End Class

    ''' <summary>Limits <see cref="Pair(Of T1, T2)"/> to contain only values of the same type</summary>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Class Pair(Of T)
        Inherits Pair(Of T, T)
        Implements ICloneable(Of Pair(Of T))
        ''' <summary>CTor - initialize with two values</summary>
        ''' <param name="V1">Initial value for <see cref="Value1"/></param>
        ''' <param name="V2">Initial value for <see cref="Value2"/></param>
        Public Sub New(ByVal V1 As T, ByVal V2 As T)
            MyBase.New(V1, V2)
        End Sub
        ''' <summary>CTor - initialize with another instance of <see cref="IPair(Of T1, T1)"/></summary>
        ''' <param name="a">Instance to initialize new istance</param>
        Public Sub New(ByVal a As IPair(Of T, T))
            MyBase.New(a)
        End Sub
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Public Shadows Function Clone() As Pair(Of T) Implements ICloneable(Of Pair(Of T)).Clone
            Return MyBase.Clone
        End Function
    End Class
#End If
End Namespace