﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.8.3928.0.
// 


namespace CarbonIntensityTypes
{
   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-6:generationloaddocument:3:0")]
   [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:iec62325.351:tc57wg16:451-6:generationloaddocument:3:0", IsNullable = false)]
   public partial class GL_MarketDocument
   {

      private string mRIDField;

      private string revisionNumberField;

      private string typeField;

      private string processprocessTypeField;

      private string sender_MarketParticipantmarketRoletypeField;

      private string receiver_MarketParticipantmarketRoletypeField;

      private string createdDateTimeField;

      private GL_MarketDocumentSender_MarketParticipantmRID[] sender_MarketParticipantmRIDField;

      private GL_MarketDocumentReceiver_MarketParticipantmRID[] receiver_MarketParticipantmRIDField;

      private GL_MarketDocumentTime_PeriodtimeInterval[] time_PeriodtimeIntervalField;

      private GL_MarketDocumentTimeSeries[] timeSeriesField;

      /// <remarks/>
      public string mRID
      {
         get
         {
            return this.mRIDField;
         }
         set
         {
            this.mRIDField = value;
         }
      }

      /// <remarks/>
      public string revisionNumber
      {
         get
         {
            return this.revisionNumberField;
         }
         set
         {
            this.revisionNumberField = value;
         }
      }

      /// <remarks/>
      public string type
      {
         get
         {
            return this.typeField;
         }
         set
         {
            this.typeField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("process.processType")]
      public string processprocessType
      {
         get
         {
            return this.processprocessTypeField;
         }
         set
         {
            this.processprocessTypeField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("sender_MarketParticipant.marketRole.type")]
      public string sender_MarketParticipantmarketRoletype
      {
         get
         {
            return this.sender_MarketParticipantmarketRoletypeField;
         }
         set
         {
            this.sender_MarketParticipantmarketRoletypeField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("receiver_MarketParticipant.marketRole.type")]
      public string receiver_MarketParticipantmarketRoletype
      {
         get
         {
            return this.receiver_MarketParticipantmarketRoletypeField;
         }
         set
         {
            this.receiver_MarketParticipantmarketRoletypeField = value;
         }
      }

      /// <remarks/>
      public string createdDateTime
      {
         get
         {
            return this.createdDateTimeField;
         }
         set
         {
            this.createdDateTimeField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("sender_MarketParticipant.mRID", IsNullable = true)]
      public GL_MarketDocumentSender_MarketParticipantmRID[] sender_MarketParticipantmRID
      {
         get
         {
            return this.sender_MarketParticipantmRIDField;
         }
         set
         {
            this.sender_MarketParticipantmRIDField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("receiver_MarketParticipant.mRID", IsNullable = true)]
      public GL_MarketDocumentReceiver_MarketParticipantmRID[] receiver_MarketParticipantmRID
      {
         get
         {
            return this.receiver_MarketParticipantmRIDField;
         }
         set
         {
            this.receiver_MarketParticipantmRIDField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("time_Period.timeInterval")]
      public GL_MarketDocumentTime_PeriodtimeInterval[] time_PeriodtimeInterval
      {
         get
         {
            return this.time_PeriodtimeIntervalField;
         }
         set
         {
            this.time_PeriodtimeIntervalField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("TimeSeries")]
      public GL_MarketDocumentTimeSeries[] TimeSeries
      {
         get
         {
            return this.timeSeriesField;
         }
         set
         {
            this.timeSeriesField = value;
         }
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-6:generationloaddocument:3:0")]
   public partial class GL_MarketDocumentSender_MarketParticipantmRID
   {

      private string codingSchemeField;

      private string valueField;

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public string codingScheme
      {
         get
         {
            return this.codingSchemeField;
         }
         set
         {
            this.codingSchemeField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlTextAttribute()]
      public string Value
      {
         get
         {
            return this.valueField;
         }
         set
         {
            this.valueField = value;
         }
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-6:generationloaddocument:3:0")]
   public partial class GL_MarketDocumentReceiver_MarketParticipantmRID
   {

      private string codingSchemeField;

      private string valueField;

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public string codingScheme
      {
         get
         {
            return this.codingSchemeField;
         }
         set
         {
            this.codingSchemeField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlTextAttribute()]
      public string Value
      {
         get
         {
            return this.valueField;
         }
         set
         {
            this.valueField = value;
         }
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-6:generationloaddocument:3:0")]
   public partial class GL_MarketDocumentTime_PeriodtimeInterval
   {

      private string startField;

      private string endField;

      /// <remarks/>
      public string start
      {
         get
         {
            return this.startField;
         }
         set
         {
            this.startField = value;
         }
      }

      /// <remarks/>
      public string end
      {
         get
         {
            return this.endField;
         }
         set
         {
            this.endField = value;
         }
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-6:generationloaddocument:3:0")]
   public partial class GL_MarketDocumentTimeSeries
   {

      private string mRIDField;

      private string businessTypeField;

      private string objectAggregationField;

      private string registeredResourcenameField;

      private string quantity_Measure_UnitnameField;

      private string curveTypeField;

      private GL_MarketDocumentTimeSeriesInBiddingZone_DomainmRID[] inBiddingZone_DomainmRIDField;

      private GL_MarketDocumentTimeSeriesRegisteredResourcemRID[] registeredResourcemRIDField;

      private GL_MarketDocumentTimeSeriesMktPSRType[] mktPSRTypeField;

      private GL_MarketDocumentTimeSeriesPeriod[] periodField;

      /// <remarks/>
      public string mRID
      {
         get
         {
            return this.mRIDField;
         }
         set
         {
            this.mRIDField = value;
         }
      }

      /// <remarks/>
      public string businessType
      {
         get
         {
            return this.businessTypeField;
         }
         set
         {
            this.businessTypeField = value;
         }
      }

      /// <remarks/>
      public string objectAggregation
      {
         get
         {
            return this.objectAggregationField;
         }
         set
         {
            this.objectAggregationField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("registeredResource.name")]
      public string registeredResourcename
      {
         get
         {
            return this.registeredResourcenameField;
         }
         set
         {
            this.registeredResourcenameField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("quantity_Measure_Unit.name")]
      public string quantity_Measure_Unitname
      {
         get
         {
            return this.quantity_Measure_UnitnameField;
         }
         set
         {
            this.quantity_Measure_UnitnameField = value;
         }
      }

      /// <remarks/>
      public string curveType
      {
         get
         {
            return this.curveTypeField;
         }
         set
         {
            this.curveTypeField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("inBiddingZone_Domain.mRID", IsNullable = true)]
      public GL_MarketDocumentTimeSeriesInBiddingZone_DomainmRID[] inBiddingZone_DomainmRID
      {
         get
         {
            return this.inBiddingZone_DomainmRIDField;
         }
         set
         {
            this.inBiddingZone_DomainmRIDField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("registeredResource.mRID", IsNullable = true)]
      public GL_MarketDocumentTimeSeriesRegisteredResourcemRID[] registeredResourcemRID
      {
         get
         {
            return this.registeredResourcemRIDField;
         }
         set
         {
            this.registeredResourcemRIDField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("MktPSRType")]
      public GL_MarketDocumentTimeSeriesMktPSRType[] MktPSRType
      {
         get
         {
            return this.mktPSRTypeField;
         }
         set
         {
            this.mktPSRTypeField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("Period")]
      public GL_MarketDocumentTimeSeriesPeriod[] Period
      {
         get
         {
            return this.periodField;
         }
         set
         {
            this.periodField = value;
         }
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-6:generationloaddocument:3:0")]
   public partial class GL_MarketDocumentTimeSeriesInBiddingZone_DomainmRID
   {

      private string codingSchemeField;

      private string valueField;

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public string codingScheme
      {
         get
         {
            return this.codingSchemeField;
         }
         set
         {
            this.codingSchemeField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlTextAttribute()]
      public string Value
      {
         get
         {
            return this.valueField;
         }
         set
         {
            this.valueField = value;
         }
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-6:generationloaddocument:3:0")]
   public partial class GL_MarketDocumentTimeSeriesRegisteredResourcemRID
   {

      private string codingSchemeField;

      private string valueField;

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public string codingScheme
      {
         get
         {
            return this.codingSchemeField;
         }
         set
         {
            this.codingSchemeField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlTextAttribute()]
      public string Value
      {
         get
         {
            return this.valueField;
         }
         set
         {
            this.valueField = value;
         }
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-6:generationloaddocument:3:0")]
   public partial class GL_MarketDocumentTimeSeriesMktPSRType
   {

      private string psrTypeField;

      private GL_MarketDocumentTimeSeriesMktPSRTypeVoltage_PowerSystemResourceshighVoltageLimit[] voltage_PowerSystemResourceshighVoltageLimitField;

      /// <remarks/>
      public string psrType
      {
         get
         {
            return this.psrTypeField;
         }
         set
         {
            this.psrTypeField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("voltage_PowerSystemResources.highVoltageLimit", IsNullable = true)]
      public GL_MarketDocumentTimeSeriesMktPSRTypeVoltage_PowerSystemResourceshighVoltageLimit[] voltage_PowerSystemResourceshighVoltageLimit
      {
         get
         {
            return this.voltage_PowerSystemResourceshighVoltageLimitField;
         }
         set
         {
            this.voltage_PowerSystemResourceshighVoltageLimitField = value;
         }
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-6:generationloaddocument:3:0")]
   public partial class GL_MarketDocumentTimeSeriesMktPSRTypeVoltage_PowerSystemResourceshighVoltageLimit
   {

      private string unitField;

      private string valueField;

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public string unit
      {
         get
         {
            return this.unitField;
         }
         set
         {
            this.unitField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlTextAttribute()]
      public string Value
      {
         get
         {
            return this.valueField;
         }
         set
         {
            this.valueField = value;
         }
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-6:generationloaddocument:3:0")]
   public partial class GL_MarketDocumentTimeSeriesPeriod
   {

      private string resolutionField;

      private GL_MarketDocumentTimeSeriesPeriodTimeInterval[] timeIntervalField;

      private GL_MarketDocumentTimeSeriesPeriodPoint[] pointField;

      /// <remarks/>
      public string resolution
      {
         get
         {
            return this.resolutionField;
         }
         set
         {
            this.resolutionField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("timeInterval")]
      public GL_MarketDocumentTimeSeriesPeriodTimeInterval[] timeInterval
      {
         get
         {
            return this.timeIntervalField;
         }
         set
         {
            this.timeIntervalField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("Point")]
      public GL_MarketDocumentTimeSeriesPeriodPoint[] Point
      {
         get
         {
            return this.pointField;
         }
         set
         {
            this.pointField = value;
         }
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-6:generationloaddocument:3:0")]
   public partial class GL_MarketDocumentTimeSeriesPeriodTimeInterval
   {

      private string startField;

      private string endField;

      /// <remarks/>
      public string start
      {
         get
         {
            return this.startField;
         }
         set
         {
            this.startField = value;
         }
      }

      /// <remarks/>
      public string end
      {
         get
         {
            return this.endField;
         }
         set
         {
            this.endField = value;
         }
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-6:generationloaddocument:3:0")]
   public partial class GL_MarketDocumentTimeSeriesPeriodPoint
   {

      private string positionField;

      private string quantityField;

      /// <remarks/>
      public string position
      {
         get
         {
            return this.positionField;
         }
         set
         {
            this.positionField = value;
         }
      }

      /// <remarks/>
      public string quantity
      {
         get
         {
            return this.quantityField;
         }
         set
         {
            this.quantityField = value;
         }
      }
   }

   /// <remarks/>
   [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:iec62325.351:tc57wg16:451-6:generationloaddocument:3:0")]
   [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:iec62325.351:tc57wg16:451-6:generationloaddocument:3:0", IsNullable = false)]
   public partial class NewDataSet
   {

      private GL_MarketDocument[] itemsField;

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("GL_MarketDocument")]
      public GL_MarketDocument[] Items
      {
         get
         {
            return this.itemsField;
         }
         set
         {
            this.itemsField = value;
         }
      }
   }
}