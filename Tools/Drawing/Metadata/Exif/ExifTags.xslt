<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:et="http://codeplex.com/DTools/ExifTags">

    <xsl:output method="text" version="1.0" encoding="UTF-8" indent="no" omit-xml-declaration="yes"/>

    <!-- parameters passed in by the TransformCodeGenerator -->
    <xsl:param name="generator"></xsl:param>
    <xsl:param name="version"></xsl:param>
    <xsl:param name="fullfilename"></xsl:param>
    <xsl:param name="filename"></xsl:param>
    <xsl:param name="date-created"></xsl:param>
    <xsl:param name="created-by"></xsl:param>
    <xsl:param name="namespace"></xsl:param>
    <xsl:param name="classname"></xsl:param>

    <xsl:template match="/">
        <xsl:call-template name="header-comment"/>
        <xsl:if test="$namespace!=''">
            <xsl:text>Namespace </xsl:text>
            <xsl:value-of select="$namespace"/>
            <xsl:call-template name="nl"/>
        </xsl:if>
        <xsl:text>#If Congig &lt;= </xsl:text>
        <xsl:value-of select="et:Root/@Stage"/>
        <xsl:if test="et:Root/@Stage!='Release'">
            <xsl:text> 'Stage: </xsl:text>
            <xsl:value-of select="et:Root/@Stage"/>
        </xsl:if>
        <xsl:call-template name="nl"/>
        <xsl:call-template name="code-gen"/>
        <xsl:text>#End If&#xD;&#xA;</xsl:text>
        <xsl:if test="$namespace!=''">
            <xsl:text>End Namespace</xsl:text>
            <xsl:call-template name="nl"/>
        </xsl:if>
    </xsl:template>
    <!--Generates end of line-->
    <xsl:template name="nl">
        <xsl:text xml:space="preserve">&#xD;&#xA;</xsl:text>
    </xsl:template>
    <!--Generates header comment-->
    <xsl:template name="header-comment">
        <xsl:text>' GENERATED FILE -- DO NOT EDIT&#xD;&#xA;</xsl:text>
        <xsl:text>'&#xD;&#xA;</xsl:text>
        <xsl:text>' Generator: </xsl:text>
        <xsl:value-of select="$generator"/>
        <xsl:call-template name="nl"/>        
        <xsl:text>' Version: </xsl:text>
        <xsl:value-of select="$version"/>
        <xsl:call-template name="nl"/>
        <xsl:text>'&#xD;&#xA;</xsl:text>
        <xsl:text>'&#xD;&#xA;</xsl:text>
        <xsl:text>' Generated code from "</xsl:text>
        <xsl:value-of select="$filename"/>
        <xsl:text>"&#xD;&#xA;</xsl:text>
        <xsl:text>'&#xD;&#xA;</xsl:text>
        <xsl:text>' Created: </xsl:text>
        <xsl:value-of select="$date-created"/>
        <xsl:call-template name="nl"/>
        <xsl:text>' By:</xsl:text>
        <xsl:value-of select="$created-by"/>
        <xsl:call-template name="nl"/>
        <xsl:text>'&#xD;&#xA;</xsl:text>
    </xsl:template>

    <xsl:template name="code-gen">
        <xsl:text>&#9;Partial Public Class Exif&#xD;&#xA;</xsl:text>
        <xsl:call-template name="Tag-enums"/>
        <xsl:text>&#9;End Class&#xD;&#xA;</xsl:text>
    </xsl:template>
    <xsl:template name="end-class">
        <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Case Else : Throw New InvalidEnumArgumentException("Tag", Tag, GetType(Tags))&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;&#9;End Select&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;End Get&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;End Property&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;End Class&#xD;&#xA;</xsl:text>
    </xsl:template>
    <xsl:template name="property-head">
        <xsl:text>&#9;&#9;&#9;''' &lt;summary>Gets format for tag specified&lt;/summary>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;''' &lt;exception cref="InvalidEnumArgumentException">&lt;paramref name="Tag"/> contains unknown value&lt;/exception>&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&lt;CLSCompliant(False)> Public ReadOnly Property TagFormat(ByVal Tag As Tags) As ExifTagFormat&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;Get&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;&#9;Const any As ushort=0&#xD;&#xA;</xsl:text>
        <xsl:text>&#9;&#9;&#9;&#9;&#9;Select Case Tag&#xD;&#xA;</xsl:text>
    </xsl:template>
    <!--For each supported IFD/Sub IFD generates partial class that contains enumeration of tag numbers used in that IFD-->
    <xsl:template name="Tag-enums">
        <!--IFD-->
        <xsl:if test="/et:Root/et:Group[@IFD='IFD']">
            <xsl:text>&#9;&#9;Partial Public Class IFDMain&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;''' &lt;summary>Tag numbers used in IFD0 and IFD1&lt;/summary>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&lt;CLSCompliant(False)> Public Enum Tags As UShort&#xD;&#xA;</xsl:text>
            <xsl:for-each select="/et:Root/et:Group[@IFD='IFD']">
                <xsl:call-template name="Tag-enum-content"/>
            </xsl:for-each>
            <xsl:text>&#9;&#9;&#9;End Enum&#xD;&#xA;</xsl:text>
            <xsl:text>#Region "Properties"&#xD;&#xA;</xsl:text> 
            <xsl:for-each select="/et:Root/et:Group[@IFD='IFD']">
                <xsl:call-template name="properties"/>
            </xsl:for-each>
            <xsl:text>#End Region&#xD;&#xA;</xsl:text>
            <xsl:call-template name="property-head"/>
            <xsl:for-each select="/et:Root/et:Group[@IFD='IFD']/et:Tag">
                <xsl:call-template name="Datatype-description"/>
            </xsl:for-each>
            <xsl:call-template name="end-class"/>            
        </xsl:if>
        <!--Exif-->
        <xsl:if test="/et:Root/et:Group[@IFD='Exif']">
            <xsl:text>&#9;&#9;Partial Public Class IFDExif&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;''' &lt;summary>Tag numbers used in Exif Sub IFD&lt;/summary>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&lt;CLSCompliant(False)> Public Enum Tags As UShort&#xD;&#xA;</xsl:text>
            <xsl:for-each select="/et:Root/et:Group[@IFD='Exif']">
                <xsl:call-template name="Tag-enum-content"/>
            </xsl:for-each>
            <xsl:text>&#9;&#9;&#9;End Enum&#xD;&#xA;</xsl:text>
            <xsl:text>#Region "Properties"&#xD;&#xA;</xsl:text>
            <xsl:for-each select="/et:Root/et:Group[@IFD='Exif']">
                <xsl:call-template name="properties"/>
            </xsl:for-each>
            <xsl:text>#End Region&#xD;&#xA;</xsl:text>
            <xsl:call-template name="property-head"/>
            <xsl:for-each select="/et:Root/et:Group[@IFD='Exif']/et:Tag">
                <xsl:call-template name="Datatype-description"/>
            </xsl:for-each>
            <xsl:call-template name="end-class"/>
        </xsl:if>
        <!--GPS-->
        <xsl:if test="/et:Root/et:Group[@IFD='GPS']">
            <xsl:text>&#9;&#9;Partial Public Class IFDGPS&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;''' &lt;summary>Tag numbers used in GPS Sub IFD&lt;/summary>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&lt;CLSCompliant(False)> Public Enum Tags As UShort&#xD;&#xA;</xsl:text>
            <xsl:for-each select="/et:Root/et:Group[@IFD='GPS']">
                <xsl:call-template name="Tag-enum-content"/>
            </xsl:for-each>
            <xsl:text>&#9;&#9;&#9;End Enum&#xD;&#xA;</xsl:text>
            <xsl:text>#Region "Properties"&#xD;&#xA;</xsl:text>
            <xsl:for-each select="/et:Root/et:Group[@IFD='GPS']">
                <xsl:call-template name="properties"/>
            </xsl:for-each>
            <xsl:text>#End Region&#xD;&#xA;</xsl:text>
            <xsl:call-template name="property-head"/>
            <xsl:for-each select="/et:Root/et:Group[@IFD='GPS']/et:Tag">
                <xsl:call-template name="Datatype-description"/>
            </xsl:for-each>
            <xsl:call-template name="end-class"/>
        </xsl:if>
        <!--Interop-->
        <xsl:if test="/et:Root/et:Group[@IFD='Interop']">
            <xsl:text>&#9;&#9;Partial Public Class IFDInterop&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;''' &lt;summary>Tag numbers used in Exif Interoperability IFD&lt;/summary>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&lt;CLSCompliant(False)> Public Enum Tags As UShort&#xD;&#xA;</xsl:text>
            <xsl:for-each select="/et:Root/et:Group[@IFD='Interop']">
                <xsl:call-template name="Tag-enum-content"/>
            </xsl:for-each>
            <xsl:text>&#9;&#9;&#9;End Enum&#xD;&#xA;</xsl:text>
            <xsl:text>#Region "Properties"&#xD;&#xA;</xsl:text>
            <xsl:for-each select="/et:Root/et:Group[@IFD='Interop']">
                <xsl:call-template name="properties"/>
            </xsl:for-each>
            <xsl:text>#End Region&#xD;&#xA;</xsl:text>
            <xsl:call-template name="property-head"/>
            <xsl:for-each select="/et:Root/et:Group[@IFD='Interop']/et:Tag">
                <xsl:call-template name="Datatype-description"/>
            </xsl:for-each>
            <xsl:call-template name="end-class"/>
        </xsl:if>
    </xsl:template>
    
    <!--Generates content of one group of tags in one IFD-->
    <xsl:template name="Tag-enum-content">
        <xsl:text>#Region "</xsl:text>
        <xsl:value-of select="@Name"/>
        <xsl:text>"&#xD;&#xA;</xsl:text>
        <xsl:for-each select="et:Tag">
            <xsl:text>&#9;&#9;&#9;&#9;''' &lt;summary></xsl:text>
            <xsl:value-of select="et:summary"/>
            <xsl:text>&lt;/summary>&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&lt;Category("</xsl:text>
            <xsl:value-of select="../@ShortName"/>
            <xsl:text>")></xsl:text>
            <xsl:value-of select="@Name"/>
            <xsl:text> = &amp;h</xsl:text>
            <xsl:value-of select="@Tag"/>
            <xsl:call-template name="nl"/>
        </xsl:for-each>
        <xsl:text>#End Region&#xD;&#xA;</xsl:text>
    </xsl:template>

    <!--Generates content of Select statement that returns description of tag datatype-->
    <xsl:template name="Datatype-description">
        <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Case Tags.</xsl:text>
        <xsl:value-of select="@Name"/>
        <xsl:text> : Return New ExifTagFormat(</xsl:text>
        <xsl:value-of select="@Components"/>
        <xsl:text>, &amp;h</xsl:text>
        <xsl:value-of select="@Tag"/>        
        <xsl:text>, "</xsl:text>
        <xsl:value-of select="@Name"/>
        <xsl:text>"</xsl:text>
        <xsl:for-each select="et:Type">
            <xsl:text>, ExifIFDReader.DirectoryEntry.ExifDataTypes.</xsl:text>
            <xsl:value-of select="."/>
        </xsl:for-each>
        <xsl:text>)&#xD;&#xA;</xsl:text>
    </xsl:template>

    <!--Generates IFD specific properties-->
    <xsl:template name="properties">
        <xsl:text>#Region "</xsl:text>
        <xsl:value-of select="@ShortName"/>
        <xsl:text>"&#xD;&#xA;</xsl:text>
        <xsl:for-each select="et:Tag">
            <!--Generate enum if exists-->
            <xsl:if test="et:enum">
                <xsl:choose>
                    <xsl:when test="count(et:enum/et:ref)>0"><!--This is not real enum, it's only reference to enum declared elsewhere--></xsl:when>
                    <xsl:otherwise>
                        <xsl:text>&#9;&#9;&#9;''' &lt;summary>Possible values of the &lt;see cref="</xsl:text>
                        <xsl:value-of select="@Name"/>                        
                        <xsl:text>"/> property&lt;/summary>&#xD;&#xA;</xsl:text>
                        <xsl:if test="et:Type[1]='UInt32' or et:Type[1]='UInt16' or et:Type[1]='URational' or et:Type[1]='SByte'">
                            <xsl:text>&#9;&#9;&#9;&lt;CLSCompliant(False)> _&#xD;&#xA;</xsl:text>
                        </xsl:if>
                        <xsl:text>&#9;&#9;&#9;Public Enum </xsl:text>
                        <xsl:value-of select="@Name"/>
                        <xsl:text>Values As </xsl:text>
                        <xsl:choose>
                            <xsl:when test="et:Type[1]='NA'">
                                <xsl:text>Byte</xsl:text>
                            </xsl:when>
                            <xsl:when test="et:Type[1]='ASCII'">
                                <xsl:text>Integer</xsl:text>
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:value-of select="et:Type[1]"/>
                            </xsl:otherwise>
                        </xsl:choose>
                        <xsl:call-template name="nl"/>
                        <xsl:for-each select="et:enum/et:item">
                            <xsl:text>&#9;&#9;&#9;&#9;''' &lt;summary></xsl:text>
                            <xsl:value-of select="@summary"/>
                            <xsl:text>&lt;/summary>&#xD;&#xA;&#9;&#9;&#9;&#9;</xsl:text>
                            <xsl:value-of select="@name"/>
                            <xsl:text> = </xsl:text>
                            <xsl:choose>
                                <xsl:when test="../../et:Type[1]='ASCII'">
                                    <xsl:text>AscW("</xsl:text>
                                    <xsl:value-of select="@value"/>
                                    <xsl:text>"c)</xsl:text>
                                </xsl:when>
                                <xsl:otherwise>
                                    <xsl:value-of select="@value"/>
                                </xsl:otherwise>
                            </xsl:choose>
                            <xsl:call-template name="nl"/>
                        </xsl:for-each>
                        <xsl:text>&#9;&#9;&#9;End Enum&#xD;&#xA;</xsl:text>
                    </xsl:otherwise>
                </xsl:choose>
            </xsl:if>
            <!--Store name of enum for future use-->
            <xsl:variable name="EnumName">
                <xsl:choose>
                    <xsl:when test="count(et:enum/et:ref)>0">
                        <xsl:value-of select="et:enum/et:ref/@ref"/>
                        <xsl:text>Values</xsl:text>
                    </xsl:when>
                    <xsl:when test="count(et:enum)>0">
                        <xsl:value-of select="@Name"/>
                        <xsl:text>Values</xsl:text>
                    </xsl:when>
                </xsl:choose>
            </xsl:variable>
            <!--Property Header-->
            <xsl:text>&#9;&#9;&#9;''' &lt;summary></xsl:text>
            <xsl:value-of select="et:summary"/>
            <xsl:text>&lt;/summary>&#xD;&#xA;</xsl:text>
            <xsl:choose>
            <xsl:when test="count(et:enum)>0 and @Components=0">
                <xsl:text>&#9;&#9;&#9;''' &lt;exception cref="InvalidEnumArgumentException">Value of &lt;paramref name="value"/> is not member of &lt;see cref="</xsl:text>
                <xsl:value-of select="$EnumName"/>
                <xsl:text>"/>&lt;/exception>&#xD;&#xA;</xsl:text>
            </xsl:when>
            <xsl:when test="count(et:enum)>0">
                <xsl:text>&#9;&#9;&#9;''' &lt;exception cref="InvalidEnumArgumentException">Value of item of &lt;paramref name="value"/> is not member of &lt;see cref="</xsl:text>
                <xsl:value-of select="$EnumName"/>
                <xsl:text>"/>&lt;/exception>&#xD;&#xA;</xsl:text>
            </xsl:when>
            </xsl:choose>
            <xsl:text>&#9;&#9;&#9;&lt;Category("</xsl:text>
            <xsl:value-of select="../@ShortName"/>
            <xsl:text>"), Description("</xsl:text>
            <xsl:value-of select="et:summary"/>
            <xsl:text>")> _&#xD;&#xA;</xsl:text>
            <xsl:if test="et:Type[1]='UInt32' or et:Type[1]='UInt16' or et:Type[1]='URational' or et:Type[1]='SByte'">
                <xsl:text>&#9;&#9;&#9;&lt;CLSCompliant(False)> _&#xD;&#xA;</xsl:text>
            </xsl:if>
            <xsl:text>&#9;&#9;&#9;Public Property </xsl:text>
            <xsl:value-of select="@Name"/>
            <xsl:text> As </xsl:text>
            <xsl:if test="@Components=1">
                <xsl:text>Nullable(Of </xsl:text>
            </xsl:if>
            <!--Generate type of property-->
            <xsl:choose>
                <xsl:when test="count(et:enum/et:ref)>0">
                    <xsl:value-of select="$EnumName"/>
                    <xsl:if test="@Components!=1 and et:Type[1]='ASCII'">
                        <xsl:text>()</xsl:text>
                    </xsl:if>
                </xsl:when>
                <xsl:when test="count(et:enum)>0">
                    <xsl:value-of select="$EnumName"/>
                    <xsl:if test="@Components!=1 and et:Type[1]='ASCII'">
                        <xsl:text>()</xsl:text>
                    </xsl:if>
                </xsl:when>
                <xsl:when test="et:Type[1]='ASCII' and @Components=1">
                    <xsl:text>Char</xsl:text>
                </xsl:when>
                <xsl:when test="et:Type[1]='ASCII'">
                    <xsl:text>String</xsl:text>
                </xsl:when>
                <xsl:when test="et:Type[1]='NA'">
                    <xsl:text>Byte</xsl:text>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:value-of select="et:Type[1]"/>
                </xsl:otherwise>
            </xsl:choose>
            <!--Is property array? Note ASCII is never array unless it is an enum-->
            <xsl:if test="et:Type[1]!='ASCII' and @Components!=1">
                <xsl:text>()</xsl:text>
            </xsl:if>
            <xsl:if test="@Components=1">
                <xsl:text>)</xsl:text>
            </xsl:if>
            <xsl:call-template name="nl"/>
            <!--Get method-->
            <xsl:text>&#9;&#9;&#9;&#9;Get&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;Dim value As ExifRecord = Record(Tags.</xsl:text>
            <xsl:value-of select="@Name"/>
            <xsl:text>)&#xD;&#xA;</xsl:text>
            <xsl:choose>
                <xsl:when test="count(et:enum)>0 and et:Type[1]='ASCII' and @Components=1">
                    <!--ASCII enum-->
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;If value Is Nothing Then Return Nothing Else Return AscW(value.Data)&#xD;&#xA;</xsl:text>                    
                </xsl:when>
                <xsl:when test="count(et:enum)>0 and et:Type[1]='ASCII'">
                    <!--Array of ASCII enums-->
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;If value Is Nothing Then&#xD;&#xA;</xsl:text>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Return Nothing&#xD;&#xA;</xsl:text>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;ElseIf value.DataType.NumberOfElements = 1 Then&#xD;&#xA;</xsl:text>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Return New </xsl:text>
                    <xsl:value-of select="$EnumName"/>
                    <xsl:text>() {AscW(CStr(value.Data))}&#xD;&#xA;</xsl:text>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;Else&#xD;&#xA;</xsl:text>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Dim ret(DirectCast(value.Data, String).Length - 1) As </xsl:text>
                    <xsl:value-of select="$EnumName"/>
                    <xsl:call-template name="nl"/>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;For i As Integer = 0 To DirectCast(value.Data, String).Length - 1&#xA;</xsl:text>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;&#9;ret(i) = AscW(DirectCast(value.Data, String)(i))&#xD;&#xA;</xsl:text>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Next i&#xD;&#xA;</xsl:text>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Return ret&#xD;&#xA;</xsl:text>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;End If&#xD;&#xA;</xsl:text>                    
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;If value Is Nothing Then Return Nothing Else Return value.Data&#xD;&#xA;</xsl:text>
                </xsl:otherwise>
            </xsl:choose>
            <xsl:text>&#9;&#9;&#9;&#9;End Get&#xD;&#xA;</xsl:text>
            <!--Set method-->
            <xsl:text>&#9;&#9;&#9;&#9;Set&#xD;&#xA;</xsl:text>
            <xsl:if test="count(et:enum)>0 and @Components=1">
                <xsl:text>&#9;&#9;&#9;&#9;&#9;If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(</xsl:text>
                <xsl:value-of select="$EnumName"/>
                <xsl:text>)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(</xsl:text>
                <xsl:value-of select="$EnumName"/>
                <xsl:text>))&#xD;&#xA;</xsl:text>                
            </xsl:if>
            <xsl:if test="count(et:enum)>0 and @Components!=1">
                <xsl:text>&#9;&#9;&#9;&#9;&#9;If value IsNot Nothing Then&#xD;&#xA;</xsl:text>
                <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;For Each itm As </xsl:text>
                <xsl:value-of select="$EnumName"/>
                <xsl:text> In value&#xD;&#xA;</xsl:text>
                <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;&#9;If Array.IndexOf([Enum].GetValues(GetType(</xsl:text>
                <xsl:value-of select="$EnumName"/>
                <xsl:text>)), itm) = -1 Then Throw New InvalidEnumArgumentException("value", itm, GetType(</xsl:text>
                <xsl:value-of select="$EnumName"/>
                <xsl:text>))&#xD;&#xA;</xsl:text>
                <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Next itm&#xD;&#xA;</xsl:text>
                <xsl:text>&#9;&#9;&#9;&#9;&#9;End If&#xD;&#xA;</xsl:text>
            </xsl:if>            
            <xsl:text>&#9;&#9;&#9;&#9;&#9;If value</xsl:text>
            <xsl:choose>
                <xsl:when  test="@Components=1">
                    <xsl:text>.HasValue</xsl:text>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text> IsNot Nothing</xsl:text>
                </xsl:otherwise>
            </xsl:choose>
            <xsl:text> Then&#xD;&#xA;</xsl:text>
            <xsl:choose>
                <xsl:when test="count(et:enum)>0 and et:Type[1]='ASCII' and @Components=1">
                    <!--ASCII enum-->
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Records(Tags.</xsl:text>
                    <xsl:value-of select="@Name"/>
                    <xsl:text>) = New ExifRecord(TagFormat(Tags.</xsl:text>
                    <xsl:value-of select="@Name"/>
                    <xsl:text>), ChrW(value), False)&#xD;&#xA;</xsl:text>
                </xsl:when>
                <xsl:when test="count(et:enum)>0 and et:Type[1]='ASCII'">
                    <!--Array of ASCII enums-->
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Dim str As String = ""&#xD;&#xA;</xsl:text>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;For Each itm As GPSLatitudeRefValues In value&#xD;&#xA;</xsl:text>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;&#9;str &amp;= ChrW(itm)&#xD;&#xA;</xsl:text>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Next itm&#xD;&#xA;</xsl:text>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Records(Tags.</xsl:text>
                    <xsl:value-of select="@Name"/>
                    <xsl:text>) = New ExifRecord(TagFormat(Tags.</xsl:text>
                    <xsl:value-of select="@Name"/>
                    <xsl:text>), Str, False)&#xD;&#xA;</xsl:text>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:text>&#9;&#9;&#9;&#9;&#9;&#9;Records(Tags.</xsl:text>
                    <xsl:value-of select="@Name"/>
                    <xsl:text>) = New ExifRecord(TagFormat(Tags.</xsl:text>
                    <xsl:value-of select="@Name"/>
                    <xsl:text>), value</xsl:text>
                    <xsl:if test="@Components=1">
                        <xsl:text>.Value</xsl:text>
                    </xsl:if>
                    <xsl:text>, </xsl:text>
                    <xsl:choose>
                        <xsl:when  test="@Components=1">
                            <xsl:text>True</xsl:text>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:text>False</xsl:text>
                        </xsl:otherwise>
                    </xsl:choose>
                    <xsl:text>)&#xD;&#xA;</xsl:text>
                </xsl:otherwise>
            </xsl:choose>
            <xsl:text>&#9;&#9;&#9;&#9;&#9;Else : Records(Tags.</xsl:text>
            <xsl:value-of select="@Name"/>
            <xsl:text>) = Nothing : End If&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;&#9;End Set&#xD;&#xA;</xsl:text>
            <xsl:text>&#9;&#9;&#9;End Property&#xD;&#xA;</xsl:text>
        </xsl:for-each>
        <xsl:text>#End Region&#xD;&#xA;</xsl:text>
    </xsl:template>
</xsl:stylesheet>