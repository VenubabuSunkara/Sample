
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class RPT_REG_CARRIER_PROFILE_XML
{

    private RPT_REG_CARRIER_PROFILE_XMLG_DOT_NUMBER g_DOT_NUMBERField;

    private RPT_REG_CARRIER_PROFILE_XMLG_VIOL_CAT_TYPE_DESC[] g_VIOL_CAT_TYPE_DESCField; //Required

    private RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_CRASH g_RPT_REG_CP_CRASHField; //Required    

    private RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP g_RPT_REG_CP_INSPField;  //Required

    /// <remarks/>
    public RPT_REG_CARRIER_PROFILE_XMLG_DOT_NUMBER G_DOT_NUMBER
    {
        get
        {
            return this.g_DOT_NUMBERField;
        }
        set
        {
            this.g_DOT_NUMBERField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("G_VIOL_CAT_TYPE_DESC")]
    public RPT_REG_CARRIER_PROFILE_XMLG_VIOL_CAT_TYPE_DESC[] G_VIOL_CAT_TYPE_DESC
    {
        get
        {
            return this.g_VIOL_CAT_TYPE_DESCField;
        }
        set
        {
            this.g_VIOL_CAT_TYPE_DESCField = value;
        }
    }

   

    /// <remarks/>
    public RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_CRASH G_RPT_REG_CP_CRASH
    {
        get
        {
            return this.g_RPT_REG_CP_CRASHField;
        }
        set
        {
            this.g_RPT_REG_CP_CRASHField = value;
        }
    }


    /// <remarks/>
    public RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP G_RPT_REG_CP_INSP
    {
        get
        {
            return this.g_RPT_REG_CP_INSPField;
        }
        set
        {
            this.g_RPT_REG_CP_INSPField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_DOT_NUMBER
{

    private string tRANS_IDField;

    private string dOT_NUMBERField;

    private RPT_REG_CARRIER_PROFILE_XMLG_DOT_NUMBERG_RPT_REG_CP_CENSUS_ID g_RPT_REG_CP_CENSUS_IDField;

    /// <remarks/>
    public string TRANS_ID
    {
        get
        {
            return this.tRANS_IDField;
        }
        set
        {
            this.tRANS_IDField = value;
        }
    }

    /// <remarks/>
    public string DOT_NUMBER
    {
        get
        {
            return this.dOT_NUMBERField;
        }
        set
        {
            this.dOT_NUMBERField = value;
        }
    }

    /// <remarks/>
    public RPT_REG_CARRIER_PROFILE_XMLG_DOT_NUMBERG_RPT_REG_CP_CENSUS_ID G_RPT_REG_CP_CENSUS_ID
    {
        get
        {
            return this.g_RPT_REG_CP_CENSUS_IDField;
        }
        set
        {
            this.g_RPT_REG_CP_CENSUS_IDField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_DOT_NUMBERG_RPT_REG_CP_CENSUS_ID
{

    private object rEVOCATION_RESCINDED_DATEField;

    private uint eINField;

    private string mAIL_STREETField;

    private string mAIL_CITYField;

    private string mAIL_STATEField;

    private ushort mAIL_ZIP_CODEField;

    private ushort tRUCK_UNITSField;

    private byte bUS_UNITSField;

    private object rEVOCATION_DATEField;

    private string nEW_ENTRANT_CODEField;

    private object nEW_ENTRANT_ENTRY_DATEField;

    private object nEW_ENTRANT_EXIT_DATEField;

    private string nEW_ENTRANT_CODE_DESCField;

    private string mCS150_DATEField;

    private string cLASS1Field;

    private object cLASS2Field;

    private object cLASS3Field;

    private object cLASS4Field;

    private object cLASS5Field;

    private object cLASS6Field;

    private object cLASS7Field;

    private object cLASS8Field;

    private object cLASS9Field;

    private object cLASS10Field;

    private object cLASS11Field;

    private object cLASS12Field;

    private string cARGO1Field;

    private object cARGO2Field;

    private object cARGO3Field;

    private string hAZMAT_C1Field;

    private string hAZMAT_C2Field;

    private object hAZMAT_C3Field;

    private object hAZMAT_S1Field;

    private object hAZMAT_S2Field;

    private object hAZMAT_S3Field;

    private uint dOT_NUMBER_DISPLAYField;

    private uint rPT_REG_CP_CENSUS_IDField;

    private string lEGAL_NAMEField;

    private object dBA_NAMEField;

    private string sTREETField;

    private string cITYField;

    private string sTATEField;

    private ushort zIP_CODEField;

    private string cOUNTY_NAMEField;

    private string sTATUS_CODEField;

    private object iNACTIVE_DATEField;

    private string cARRIER_TYPEField;

    private string cARRIER_OPERATION_DESCField;

    private object sHIPPER_OPERATION_DESCField;

    private ushort dRIVERSField;

    private ushort pOWER_UNITSField;

    private string sAFETY_RATINGField;

    private string rATING_DATEField;

    private object rEVIEW_DATEField;

    private ushort mILEAGE_YEARField;

    private uint cARRIER_REPORTED_MILEAGEField;

    private object g_LIST_OOSField;

    private string pHONEField;

    /// <remarks/>
    public object REVOCATION_RESCINDED_DATE
    {
        get
        {
            return this.rEVOCATION_RESCINDED_DATEField;
        }
        set
        {
            this.rEVOCATION_RESCINDED_DATEField = value;
        }
    }

    /// <remarks/>
    public uint EIN
    {
        get
        {
            return this.eINField;
        }
        set
        {
            this.eINField = value;
        }
    }

    /// <remarks/>
    public string MAIL_STREET
    {
        get
        {
            return this.mAIL_STREETField;
        }
        set
        {
            this.mAIL_STREETField = value;
        }
    }

    /// <remarks/>
    public string MAIL_CITY
    {
        get
        {
            return this.mAIL_CITYField;
        }
        set
        {
            this.mAIL_CITYField = value;
        }
    }

    /// <remarks/>
    public string MAIL_STATE
    {
        get
        {
            return this.mAIL_STATEField;
        }
        set
        {
            this.mAIL_STATEField = value;
        }
    }

    /// <remarks/>
    public ushort MAIL_ZIP_CODE
    {
        get
        {
            return this.mAIL_ZIP_CODEField;
        }
        set
        {
            this.mAIL_ZIP_CODEField = value;
        }
    }

    /// <remarks/>
    public ushort TRUCK_UNITS
    {
        get
        {
            return this.tRUCK_UNITSField;
        }
        set
        {
            this.tRUCK_UNITSField = value;
        }
    }

    /// <remarks/>
    public byte BUS_UNITS
    {
        get
        {
            return this.bUS_UNITSField;
        }
        set
        {
            this.bUS_UNITSField = value;
        }
    }

    /// <remarks/>
    public object REVOCATION_DATE
    {
        get
        {
            return this.rEVOCATION_DATEField;
        }
        set
        {
            this.rEVOCATION_DATEField = value;
        }
    }

    /// <remarks/>
    public string NEW_ENTRANT_CODE
    {
        get
        {
            return this.nEW_ENTRANT_CODEField;
        }
        set
        {
            this.nEW_ENTRANT_CODEField = value;
        }
    }

    /// <remarks/>
    public object NEW_ENTRANT_ENTRY_DATE
    {
        get
        {
            return this.nEW_ENTRANT_ENTRY_DATEField;
        }
        set
        {
            this.nEW_ENTRANT_ENTRY_DATEField = value;
        }
    }

    /// <remarks/>
    public object NEW_ENTRANT_EXIT_DATE
    {
        get
        {
            return this.nEW_ENTRANT_EXIT_DATEField;
        }
        set
        {
            this.nEW_ENTRANT_EXIT_DATEField = value;
        }
    }

    /// <remarks/>
    public string NEW_ENTRANT_CODE_DESC
    {
        get
        {
            return this.nEW_ENTRANT_CODE_DESCField;
        }
        set
        {
            this.nEW_ENTRANT_CODE_DESCField = value;
        }
    }

    /// <remarks/>
    public string MCS150_DATE
    {
        get
        {
            return this.mCS150_DATEField;
        }
        set
        {
            this.mCS150_DATEField = value;
        }
    }

    /// <remarks/>
    public string CLASS1
    {
        get
        {
            return this.cLASS1Field;
        }
        set
        {
            this.cLASS1Field = value;
        }
    }

    /// <remarks/>
    public object CLASS2
    {
        get
        {
            return this.cLASS2Field;
        }
        set
        {
            this.cLASS2Field = value;
        }
    }

    /// <remarks/>
    public object CLASS3
    {
        get
        {
            return this.cLASS3Field;
        }
        set
        {
            this.cLASS3Field = value;
        }
    }

    /// <remarks/>
    public object CLASS4
    {
        get
        {
            return this.cLASS4Field;
        }
        set
        {
            this.cLASS4Field = value;
        }
    }

    /// <remarks/>
    public object CLASS5
    {
        get
        {
            return this.cLASS5Field;
        }
        set
        {
            this.cLASS5Field = value;
        }
    }

    /// <remarks/>
    public object CLASS6
    {
        get
        {
            return this.cLASS6Field;
        }
        set
        {
            this.cLASS6Field = value;
        }
    }

    /// <remarks/>
    public object CLASS7
    {
        get
        {
            return this.cLASS7Field;
        }
        set
        {
            this.cLASS7Field = value;
        }
    }

    /// <remarks/>
    public object CLASS8
    {
        get
        {
            return this.cLASS8Field;
        }
        set
        {
            this.cLASS8Field = value;
        }
    }

    /// <remarks/>
    public object CLASS9
    {
        get
        {
            return this.cLASS9Field;
        }
        set
        {
            this.cLASS9Field = value;
        }
    }

    /// <remarks/>
    public object CLASS10
    {
        get
        {
            return this.cLASS10Field;
        }
        set
        {
            this.cLASS10Field = value;
        }
    }

    /// <remarks/>
    public object CLASS11
    {
        get
        {
            return this.cLASS11Field;
        }
        set
        {
            this.cLASS11Field = value;
        }
    }

    /// <remarks/>
    public object CLASS12
    {
        get
        {
            return this.cLASS12Field;
        }
        set
        {
            this.cLASS12Field = value;
        }
    }

    /// <remarks/>
    public string CARGO1
    {
        get
        {
            return this.cARGO1Field;
        }
        set
        {
            this.cARGO1Field = value;
        }
    }

    /// <remarks/>
    public object CARGO2
    {
        get
        {
            return this.cARGO2Field;
        }
        set
        {
            this.cARGO2Field = value;
        }
    }

    /// <remarks/>
    public object CARGO3
    {
        get
        {
            return this.cARGO3Field;
        }
        set
        {
            this.cARGO3Field = value;
        }
    }

    /// <remarks/>
    public string HAZMAT_C1
    {
        get
        {
            return this.hAZMAT_C1Field;
        }
        set
        {
            this.hAZMAT_C1Field = value;
        }
    }

    /// <remarks/>
    public string HAZMAT_C2
    {
        get
        {
            return this.hAZMAT_C2Field;
        }
        set
        {
            this.hAZMAT_C2Field = value;
        }
    }

    /// <remarks/>
    public object HAZMAT_C3
    {
        get
        {
            return this.hAZMAT_C3Field;
        }
        set
        {
            this.hAZMAT_C3Field = value;
        }
    }

    /// <remarks/>
    public object HAZMAT_S1
    {
        get
        {
            return this.hAZMAT_S1Field;
        }
        set
        {
            this.hAZMAT_S1Field = value;
        }
    }

    /// <remarks/>
    public object HAZMAT_S2
    {
        get
        {
            return this.hAZMAT_S2Field;
        }
        set
        {
            this.hAZMAT_S2Field = value;
        }
    }

    /// <remarks/>
    public object HAZMAT_S3
    {
        get
        {
            return this.hAZMAT_S3Field;
        }
        set
        {
            this.hAZMAT_S3Field = value;
        }
    }

    /// <remarks/>
    public uint DOT_NUMBER_DISPLAY
    {
        get
        {
            return this.dOT_NUMBER_DISPLAYField;
        }
        set
        {
            this.dOT_NUMBER_DISPLAYField = value;
        }
    }

    /// <remarks/>
    public uint RPT_REG_CP_CENSUS_ID
    {
        get
        {
            return this.rPT_REG_CP_CENSUS_IDField;
        }
        set
        {
            this.rPT_REG_CP_CENSUS_IDField = value;
        }
    }

    /// <remarks/>
    public string LEGAL_NAME
    {
        get
        {
            return this.lEGAL_NAMEField;
        }
        set
        {
            this.lEGAL_NAMEField = value;
        }
    }

    /// <remarks/>
    public object DBA_NAME
    {
        get
        {
            return this.dBA_NAMEField;
        }
        set
        {
            this.dBA_NAMEField = value;
        }
    }

    /// <remarks/>
    public string STREET
    {
        get
        {
            return this.sTREETField;
        }
        set
        {
            this.sTREETField = value;
        }
    }

    /// <remarks/>
    public string CITY
    {
        get
        {
            return this.cITYField;
        }
        set
        {
            this.cITYField = value;
        }
    }

    /// <remarks/>
    public string STATE
    {
        get
        {
            return this.sTATEField;
        }
        set
        {
            this.sTATEField = value;
        }
    }

    /// <remarks/>
    public ushort ZIP_CODE
    {
        get
        {
            return this.zIP_CODEField;
        }
        set
        {
            this.zIP_CODEField = value;
        }
    }

    /// <remarks/>
    public string COUNTY_NAME
    {
        get
        {
            return this.cOUNTY_NAMEField;
        }
        set
        {
            this.cOUNTY_NAMEField = value;
        }
    }

    /// <remarks/>
    public string STATUS_CODE
    {
        get
        {
            return this.sTATUS_CODEField;
        }
        set
        {
            this.sTATUS_CODEField = value;
        }
    }

    /// <remarks/>
    public object INACTIVE_DATE
    {
        get
        {
            return this.iNACTIVE_DATEField;
        }
        set
        {
            this.iNACTIVE_DATEField = value;
        }
    }

    /// <remarks/>
    public string CARRIER_TYPE
    {
        get
        {
            return this.cARRIER_TYPEField;
        }
        set
        {
            this.cARRIER_TYPEField = value;
        }
    }

    /// <remarks/>
    public string CARRIER_OPERATION_DESC
    {
        get
        {
            return this.cARRIER_OPERATION_DESCField;
        }
        set
        {
            this.cARRIER_OPERATION_DESCField = value;
        }
    }

    /// <remarks/>
    public object SHIPPER_OPERATION_DESC
    {
        get
        {
            return this.sHIPPER_OPERATION_DESCField;
        }
        set
        {
            this.sHIPPER_OPERATION_DESCField = value;
        }
    }

    /// <remarks/>
    public ushort DRIVERS
    {
        get
        {
            return this.dRIVERSField;
        }
        set
        {
            this.dRIVERSField = value;
        }
    }

    /// <remarks/>
    public ushort POWER_UNITS
    {
        get
        {
            return this.pOWER_UNITSField;
        }
        set
        {
            this.pOWER_UNITSField = value;
        }
    }

    /// <remarks/>
    public string SAFETY_RATING
    {
        get
        {
            return this.sAFETY_RATINGField;
        }
        set
        {
            this.sAFETY_RATINGField = value;
        }
    }

    /// <remarks/>
    public string RATING_DATE
    {
        get
        {
            return this.rATING_DATEField;
        }
        set
        {
            this.rATING_DATEField = value;
        }
    }

    /// <remarks/>
    public object REVIEW_DATE
    {
        get
        {
            return this.rEVIEW_DATEField;
        }
        set
        {
            this.rEVIEW_DATEField = value;
        }
    }

    /// <remarks/>
    public ushort MILEAGE_YEAR
    {
        get
        {
            return this.mILEAGE_YEARField;
        }
        set
        {
            this.mILEAGE_YEARField = value;
        }
    }

    /// <remarks/>
    public uint CARRIER_REPORTED_MILEAGE
    {
        get
        {
            return this.cARRIER_REPORTED_MILEAGEField;
        }
        set
        {
            this.cARRIER_REPORTED_MILEAGEField = value;
        }
    }

    /// <remarks/>
    public object G_LIST_OOS
    {
        get
        {
            return this.g_LIST_OOSField;
        }
        set
        {
            this.g_LIST_OOSField = value;
        }
    }

    /// <remarks/>
    public string PHONE
    {
        get
        {
            return this.pHONEField;
        }
        set
        {
            this.pHONEField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP_VIOL_SUM
{

    private string tRANS_ID10Field;

    private uint dOT_NUMBER10Field;

    private RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP_VIOL_SUMG_RPT_REG_CP_INSP_VIOL_SUM_ID[] g_RPT_REG_CP_INSP_VIOL_SUM_IDField;

    private byte cS_DRIVER_INSP_TOTALField;

    private byte cS_VEHICLE_INSP_TOTALField;

    private byte cS_HM_INSP_TOTALField;

    private byte cS_ALL_INSP_TOTALField;

    private byte cS_DRIVER_OOS_INSP_TOTALField;

    private byte cS_VEHICLE_OOS_INSP_TOTALField;

    private byte cS_HM_OOS_INSP_TOTALField;

    private byte cS_ALL_OOS_INSP_TOTALField;

    private byte cS_DRIVER_OOS_VIOL_TOTALField;

    private byte cS_VEHICLE_OOS_VIOL_TOTALField;

    private byte cS_HM_OOS_VIOL_TOTALField;

    private byte cS_ALL_OOS_VIOL_TOTALField;

    private decimal cS_DRIVER_OOS_PRC_TOTALField;

    private decimal cS_VEHICLE_OOS_PRC_TOTALField;

    private byte cS_HM_OOS_PRC_TOTALField;

    private decimal cS_DRIVER_VIOL_AVG_TOTALField;

    private decimal cS_VEHICLE_VIOL_AVG_TOTALField;

    private byte cS_HM_VIOL_AVG_TOTALField;

    private decimal cS_ALL_VIOL_AVG_TOTALField;

    /// <remarks/>
    public string TRANS_ID10
    {
        get
        {
            return this.tRANS_ID10Field;
        }
        set
        {
            this.tRANS_ID10Field = value;
        }
    }

    /// <remarks/>
    public uint DOT_NUMBER10
    {
        get
        {
            return this.dOT_NUMBER10Field;
        }
        set
        {
            this.dOT_NUMBER10Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("G_RPT_REG_CP_INSP_VIOL_SUM_ID")]
    public RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP_VIOL_SUMG_RPT_REG_CP_INSP_VIOL_SUM_ID[] G_RPT_REG_CP_INSP_VIOL_SUM_ID
    {
        get
        {
            return this.g_RPT_REG_CP_INSP_VIOL_SUM_IDField;
        }
        set
        {
            this.g_RPT_REG_CP_INSP_VIOL_SUM_IDField = value;
        }
    }

    /// <remarks/>
    public byte CS_DRIVER_INSP_TOTAL
    {
        get
        {
            return this.cS_DRIVER_INSP_TOTALField;
        }
        set
        {
            this.cS_DRIVER_INSP_TOTALField = value;
        }
    }

    /// <remarks/>
    public byte CS_VEHICLE_INSP_TOTAL
    {
        get
        {
            return this.cS_VEHICLE_INSP_TOTALField;
        }
        set
        {
            this.cS_VEHICLE_INSP_TOTALField = value;
        }
    }

    /// <remarks/>
    public byte CS_HM_INSP_TOTAL
    {
        get
        {
            return this.cS_HM_INSP_TOTALField;
        }
        set
        {
            this.cS_HM_INSP_TOTALField = value;
        }
    }

    /// <remarks/>
    public byte CS_ALL_INSP_TOTAL
    {
        get
        {
            return this.cS_ALL_INSP_TOTALField;
        }
        set
        {
            this.cS_ALL_INSP_TOTALField = value;
        }
    }

    /// <remarks/>
    public byte CS_DRIVER_OOS_INSP_TOTAL
    {
        get
        {
            return this.cS_DRIVER_OOS_INSP_TOTALField;
        }
        set
        {
            this.cS_DRIVER_OOS_INSP_TOTALField = value;
        }
    }

    /// <remarks/>
    public byte CS_VEHICLE_OOS_INSP_TOTAL
    {
        get
        {
            return this.cS_VEHICLE_OOS_INSP_TOTALField;
        }
        set
        {
            this.cS_VEHICLE_OOS_INSP_TOTALField = value;
        }
    }

    /// <remarks/>
    public byte CS_HM_OOS_INSP_TOTAL
    {
        get
        {
            return this.cS_HM_OOS_INSP_TOTALField;
        }
        set
        {
            this.cS_HM_OOS_INSP_TOTALField = value;
        }
    }

    /// <remarks/>
    public byte CS_ALL_OOS_INSP_TOTAL
    {
        get
        {
            return this.cS_ALL_OOS_INSP_TOTALField;
        }
        set
        {
            this.cS_ALL_OOS_INSP_TOTALField = value;
        }
    }

    /// <remarks/>
    public byte CS_DRIVER_OOS_VIOL_TOTAL
    {
        get
        {
            return this.cS_DRIVER_OOS_VIOL_TOTALField;
        }
        set
        {
            this.cS_DRIVER_OOS_VIOL_TOTALField = value;
        }
    }

    /// <remarks/>
    public byte CS_VEHICLE_OOS_VIOL_TOTAL
    {
        get
        {
            return this.cS_VEHICLE_OOS_VIOL_TOTALField;
        }
        set
        {
            this.cS_VEHICLE_OOS_VIOL_TOTALField = value;
        }
    }

    /// <remarks/>
    public byte CS_HM_OOS_VIOL_TOTAL
    {
        get
        {
            return this.cS_HM_OOS_VIOL_TOTALField;
        }
        set
        {
            this.cS_HM_OOS_VIOL_TOTALField = value;
        }
    }

    /// <remarks/>
    public byte CS_ALL_OOS_VIOL_TOTAL
    {
        get
        {
            return this.cS_ALL_OOS_VIOL_TOTALField;
        }
        set
        {
            this.cS_ALL_OOS_VIOL_TOTALField = value;
        }
    }

    /// <remarks/>
    public decimal CS_DRIVER_OOS_PRC_TOTAL
    {
        get
        {
            return this.cS_DRIVER_OOS_PRC_TOTALField;
        }
        set
        {
            this.cS_DRIVER_OOS_PRC_TOTALField = value;
        }
    }

    /// <remarks/>
    public decimal CS_VEHICLE_OOS_PRC_TOTAL
    {
        get
        {
            return this.cS_VEHICLE_OOS_PRC_TOTALField;
        }
        set
        {
            this.cS_VEHICLE_OOS_PRC_TOTALField = value;
        }
    }

    /// <remarks/>
    public byte CS_HM_OOS_PRC_TOTAL
    {
        get
        {
            return this.cS_HM_OOS_PRC_TOTALField;
        }
        set
        {
            this.cS_HM_OOS_PRC_TOTALField = value;
        }
    }

    /// <remarks/>
    public decimal CS_DRIVER_VIOL_AVG_TOTAL
    {
        get
        {
            return this.cS_DRIVER_VIOL_AVG_TOTALField;
        }
        set
        {
            this.cS_DRIVER_VIOL_AVG_TOTALField = value;
        }
    }

    /// <remarks/>
    public decimal CS_VEHICLE_VIOL_AVG_TOTAL
    {
        get
        {
            return this.cS_VEHICLE_VIOL_AVG_TOTALField;
        }
        set
        {
            this.cS_VEHICLE_VIOL_AVG_TOTALField = value;
        }
    }

    /// <remarks/>
    public byte CS_HM_VIOL_AVG_TOTAL
    {
        get
        {
            return this.cS_HM_VIOL_AVG_TOTALField;
        }
        set
        {
            this.cS_HM_VIOL_AVG_TOTALField = value;
        }
    }

    /// <remarks/>
    public decimal CS_ALL_VIOL_AVG_TOTAL
    {
        get
        {
            return this.cS_ALL_VIOL_AVG_TOTALField;
        }
        set
        {
            this.cS_ALL_VIOL_AVG_TOTALField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP_VIOL_SUMG_RPT_REG_CP_INSP_VIOL_SUM_ID
{

    private uint rPT_REG_CP_INSP_SUM_IDField;

    private string iNSP_YEARField;

    private byte dRIVER_INSPField;

    private byte vEHICLE_INSPField;

    private byte hM_INSPField;

    private byte aLL_INSPField;

    private byte dRIVER_OOS_INSPField;

    private byte vEHICLE_OOS_INSPField;

    private byte hM_OOS_INSPField;

    private byte aLL_OOS_INSPField;

    private byte dRIVER_OOS_VIOLField;

    private byte vEHICLE_OOS_VIOLField;

    private byte hM_OOS_VIOLField;

    private byte aLL_OOS_VIOLField;

    private decimal cF_DRIVER_OOS_PRCField;

    private decimal cF_VEHICLE_OOS_PRCField;

    private byte cF_HM_OOS_PRCField;

    private decimal cF_DRIVER_VIOL_AVGField;

    private decimal cF_VEHICLE_VIOL_AVGField;

    private byte cF_HM_VIOL_AVGField;

    private decimal cF_ALL_VIOL_AVGField;

    /// <remarks/>
    public uint RPT_REG_CP_INSP_SUM_ID
    {
        get
        {
            return this.rPT_REG_CP_INSP_SUM_IDField;
        }
        set
        {
            this.rPT_REG_CP_INSP_SUM_IDField = value;
        }
    }

    /// <remarks/>
    public string INSP_YEAR
    {
        get
        {
            return this.iNSP_YEARField;
        }
        set
        {
            this.iNSP_YEARField = value;
        }
    }

    /// <remarks/>
    public byte DRIVER_INSP
    {
        get
        {
            return this.dRIVER_INSPField;
        }
        set
        {
            this.dRIVER_INSPField = value;
        }
    }

    /// <remarks/>
    public byte VEHICLE_INSP
    {
        get
        {
            return this.vEHICLE_INSPField;
        }
        set
        {
            this.vEHICLE_INSPField = value;
        }
    }

    /// <remarks/>
    public byte HM_INSP
    {
        get
        {
            return this.hM_INSPField;
        }
        set
        {
            this.hM_INSPField = value;
        }
    }

    /// <remarks/>
    public byte ALL_INSP
    {
        get
        {
            return this.aLL_INSPField;
        }
        set
        {
            this.aLL_INSPField = value;
        }
    }

    /// <remarks/>
    public byte DRIVER_OOS_INSP
    {
        get
        {
            return this.dRIVER_OOS_INSPField;
        }
        set
        {
            this.dRIVER_OOS_INSPField = value;
        }
    }

    /// <remarks/>
    public byte VEHICLE_OOS_INSP
    {
        get
        {
            return this.vEHICLE_OOS_INSPField;
        }
        set
        {
            this.vEHICLE_OOS_INSPField = value;
        }
    }

    /// <remarks/>
    public byte HM_OOS_INSP
    {
        get
        {
            return this.hM_OOS_INSPField;
        }
        set
        {
            this.hM_OOS_INSPField = value;
        }
    }

    /// <remarks/>
    public byte ALL_OOS_INSP
    {
        get
        {
            return this.aLL_OOS_INSPField;
        }
        set
        {
            this.aLL_OOS_INSPField = value;
        }
    }

    /// <remarks/>
    public byte DRIVER_OOS_VIOL
    {
        get
        {
            return this.dRIVER_OOS_VIOLField;
        }
        set
        {
            this.dRIVER_OOS_VIOLField = value;
        }
    }

    /// <remarks/>
    public byte VEHICLE_OOS_VIOL
    {
        get
        {
            return this.vEHICLE_OOS_VIOLField;
        }
        set
        {
            this.vEHICLE_OOS_VIOLField = value;
        }
    }

    /// <remarks/>
    public byte HM_OOS_VIOL
    {
        get
        {
            return this.hM_OOS_VIOLField;
        }
        set
        {
            this.hM_OOS_VIOLField = value;
        }
    }

    /// <remarks/>
    public byte ALL_OOS_VIOL
    {
        get
        {
            return this.aLL_OOS_VIOLField;
        }
        set
        {
            this.aLL_OOS_VIOLField = value;
        }
    }

    /// <remarks/>
    public decimal CF_DRIVER_OOS_PRC
    {
        get
        {
            return this.cF_DRIVER_OOS_PRCField;
        }
        set
        {
            this.cF_DRIVER_OOS_PRCField = value;
        }
    }

    /// <remarks/>
    public decimal CF_VEHICLE_OOS_PRC
    {
        get
        {
            return this.cF_VEHICLE_OOS_PRCField;
        }
        set
        {
            this.cF_VEHICLE_OOS_PRCField = value;
        }
    }

    /// <remarks/>
    public byte CF_HM_OOS_PRC
    {
        get
        {
            return this.cF_HM_OOS_PRCField;
        }
        set
        {
            this.cF_HM_OOS_PRCField = value;
        }
    }

    /// <remarks/>
    public decimal CF_DRIVER_VIOL_AVG
    {
        get
        {
            return this.cF_DRIVER_VIOL_AVGField;
        }
        set
        {
            this.cF_DRIVER_VIOL_AVGField = value;
        }
    }

    /// <remarks/>
    public decimal CF_VEHICLE_VIOL_AVG
    {
        get
        {
            return this.cF_VEHICLE_VIOL_AVGField;
        }
        set
        {
            this.cF_VEHICLE_VIOL_AVGField = value;
        }
    }

    /// <remarks/>
    public byte CF_HM_VIOL_AVG
    {
        get
        {
            return this.cF_HM_VIOL_AVGField;
        }
        set
        {
            this.cF_HM_VIOL_AVGField = value;
        }
    }

    /// <remarks/>
    public decimal CF_ALL_VIOL_AVG
    {
        get
        {
            return this.cF_ALL_VIOL_AVGField;
        }
        set
        {
            this.cF_ALL_VIOL_AVGField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_CRASH_SUM
{

    private string tRANS_ID8Field;

    private uint dOT_NUMBER8Field;

    private RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_CRASH_SUMG_RPT_REG_CP_CRASH_SUM_ID[] g_RPT_REG_CP_CRASH_SUM_IDField;

    private byte cF_TOTALField;

    private byte cF_TOTAL_AVGField;

    private byte cS_3_YEAR_FATALField;

    private byte cS_3_YEAR_INJURYField;

    private byte cS_3_YEAR_TOWAWAYField;

    private decimal cF_3_YEAR_FATAL_AVGField;

    private decimal cF_3_YEAR_INJURY_AVGField;

    private byte cF_3_YEAR_TOWAWAY_AVGField;

    /// <remarks/>
    public string TRANS_ID8
    {
        get
        {
            return this.tRANS_ID8Field;
        }
        set
        {
            this.tRANS_ID8Field = value;
        }
    }

    /// <remarks/>
    public uint DOT_NUMBER8
    {
        get
        {
            return this.dOT_NUMBER8Field;
        }
        set
        {
            this.dOT_NUMBER8Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("G_RPT_REG_CP_CRASH_SUM_ID")]
    public RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_CRASH_SUMG_RPT_REG_CP_CRASH_SUM_ID[] G_RPT_REG_CP_CRASH_SUM_ID
    {
        get
        {
            return this.g_RPT_REG_CP_CRASH_SUM_IDField;
        }
        set
        {
            this.g_RPT_REG_CP_CRASH_SUM_IDField = value;
        }
    }

    /// <remarks/>
    public byte CF_TOTAL
    {
        get
        {
            return this.cF_TOTALField;
        }
        set
        {
            this.cF_TOTALField = value;
        }
    }

    /// <remarks/>
    public byte CF_TOTAL_AVG
    {
        get
        {
            return this.cF_TOTAL_AVGField;
        }
        set
        {
            this.cF_TOTAL_AVGField = value;
        }
    }

    /// <remarks/>
    public byte CS_3_YEAR_FATAL
    {
        get
        {
            return this.cS_3_YEAR_FATALField;
        }
        set
        {
            this.cS_3_YEAR_FATALField = value;
        }
    }

    /// <remarks/>
    public byte CS_3_YEAR_INJURY
    {
        get
        {
            return this.cS_3_YEAR_INJURYField;
        }
        set
        {
            this.cS_3_YEAR_INJURYField = value;
        }
    }

    /// <remarks/>
    public byte CS_3_YEAR_TOWAWAY
    {
        get
        {
            return this.cS_3_YEAR_TOWAWAYField;
        }
        set
        {
            this.cS_3_YEAR_TOWAWAYField = value;
        }
    }

    /// <remarks/>
    public decimal CF_3_YEAR_FATAL_AVG
    {
        get
        {
            return this.cF_3_YEAR_FATAL_AVGField;
        }
        set
        {
            this.cF_3_YEAR_FATAL_AVGField = value;
        }
    }

    /// <remarks/>
    public decimal CF_3_YEAR_INJURY_AVG
    {
        get
        {
            return this.cF_3_YEAR_INJURY_AVGField;
        }
        set
        {
            this.cF_3_YEAR_INJURY_AVGField = value;
        }
    }

    /// <remarks/>
    public byte CF_3_YEAR_TOWAWAY_AVG
    {
        get
        {
            return this.cF_3_YEAR_TOWAWAY_AVGField;
        }
        set
        {
            this.cF_3_YEAR_TOWAWAY_AVGField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_CRASH_SUMG_RPT_REG_CP_CRASH_SUM_ID
{

    private uint rPT_REG_CP_CRASH_SUM_IDField;

    private ushort cRASH_YEARField;

    private byte fATALITIESField;

    private byte iNJURIESField;

    private byte tOW_AWAYField;

    private byte cF_TOTAL_YEARField;

    /// <remarks/>
    public uint RPT_REG_CP_CRASH_SUM_ID
    {
        get
        {
            return this.rPT_REG_CP_CRASH_SUM_IDField;
        }
        set
        {
            this.rPT_REG_CP_CRASH_SUM_IDField = value;
        }
    }

    /// <remarks/>
    public ushort CRASH_YEAR
    {
        get
        {
            return this.cRASH_YEARField;
        }
        set
        {
            this.cRASH_YEARField = value;
        }
    }

    /// <remarks/>
    public byte FATALITIES
    {
        get
        {
            return this.fATALITIESField;
        }
        set
        {
            this.fATALITIESField = value;
        }
    }

    /// <remarks/>
    public byte INJURIES
    {
        get
        {
            return this.iNJURIESField;
        }
        set
        {
            this.iNJURIESField = value;
        }
    }

    /// <remarks/>
    public byte TOW_AWAY
    {
        get
        {
            return this.tOW_AWAYField;
        }
        set
        {
            this.tOW_AWAYField = value;
        }
    }

    /// <remarks/>
    public byte CF_TOTAL_YEAR
    {
        get
        {
            return this.cF_TOTAL_YEARField;
        }
        set
        {
            this.cF_TOTAL_YEARField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_L_I
{

    private string tRANS_ID5Field;

    private uint dOT_NUMBER5Field;

    private RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_L_IG_RPT_REG_CP_L_I_ID g_RPT_REG_CP_L_I_IDField;

    /// <remarks/>
    public string TRANS_ID5
    {
        get
        {
            return this.tRANS_ID5Field;
        }
        set
        {
            this.tRANS_ID5Field = value;
        }
    }

    /// <remarks/>
    public uint DOT_NUMBER5
    {
        get
        {
            return this.dOT_NUMBER5Field;
        }
        set
        {
            this.dOT_NUMBER5Field = value;
        }
    }

    /// <remarks/>
    public RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_L_IG_RPT_REG_CP_L_I_ID G_RPT_REG_CP_L_I_ID
    {
        get
        {
            return this.g_RPT_REG_CP_L_I_IDField;
        }
        set
        {
            this.g_RPT_REG_CP_L_I_IDField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_L_IG_RPT_REG_CP_L_I_ID
{

    private uint rPT_REG_CP_L_I_IDField;

    private string pREFIXField;

    private uint dOCKET_NUMBERField;

    private string lEGAL_NAME1Field;

    private object dBA_NAME1Field;

    private string cOMMON_STATField;

    private string cONTRACT_STATField;

    private string bROKER_STATField;

    private ushort mIN_COV_AMOUNTField;

    private string bIPD_REQField;

    private string bIPD_OKField;

    private string cARGO_REQField;

    private string cARGO_OKField;

    private string bOND_REQField;

    private string bOND_OKField;

    private string cF_LIABILITY_INSURANCEField;

    /// <remarks/>
    public uint RPT_REG_CP_L_I_ID
    {
        get
        {
            return this.rPT_REG_CP_L_I_IDField;
        }
        set
        {
            this.rPT_REG_CP_L_I_IDField = value;
        }
    }

    /// <remarks/>
    public string PREFIX
    {
        get
        {
            return this.pREFIXField;
        }
        set
        {
            this.pREFIXField = value;
        }
    }

    /// <remarks/>
    public uint DOCKET_NUMBER
    {
        get
        {
            return this.dOCKET_NUMBERField;
        }
        set
        {
            this.dOCKET_NUMBERField = value;
        }
    }

    /// <remarks/>
    public string LEGAL_NAME1
    {
        get
        {
            return this.lEGAL_NAME1Field;
        }
        set
        {
            this.lEGAL_NAME1Field = value;
        }
    }

    /// <remarks/>
    public object DBA_NAME1
    {
        get
        {
            return this.dBA_NAME1Field;
        }
        set
        {
            this.dBA_NAME1Field = value;
        }
    }

    /// <remarks/>
    public string COMMON_STAT
    {
        get
        {
            return this.cOMMON_STATField;
        }
        set
        {
            this.cOMMON_STATField = value;
        }
    }

    /// <remarks/>
    public string CONTRACT_STAT
    {
        get
        {
            return this.cONTRACT_STATField;
        }
        set
        {
            this.cONTRACT_STATField = value;
        }
    }

    /// <remarks/>
    public string BROKER_STAT
    {
        get
        {
            return this.bROKER_STATField;
        }
        set
        {
            this.bROKER_STATField = value;
        }
    }

    /// <remarks/>
    public ushort MIN_COV_AMOUNT
    {
        get
        {
            return this.mIN_COV_AMOUNTField;
        }
        set
        {
            this.mIN_COV_AMOUNTField = value;
        }
    }

    /// <remarks/>
    public string BIPD_REQ
    {
        get
        {
            return this.bIPD_REQField;
        }
        set
        {
            this.bIPD_REQField = value;
        }
    }

    /// <remarks/>
    public string BIPD_OK
    {
        get
        {
            return this.bIPD_OKField;
        }
        set
        {
            this.bIPD_OKField = value;
        }
    }

    /// <remarks/>
    public string CARGO_REQ
    {
        get
        {
            return this.cARGO_REQField;
        }
        set
        {
            this.cARGO_REQField = value;
        }
    }

    /// <remarks/>
    public string CARGO_OK
    {
        get
        {
            return this.cARGO_OKField;
        }
        set
        {
            this.cARGO_OKField = value;
        }
    }

    /// <remarks/>
    public string BOND_REQ
    {
        get
        {
            return this.bOND_REQField;
        }
        set
        {
            this.bOND_REQField = value;
        }
    }

    /// <remarks/>
    public string BOND_OK
    {
        get
        {
            return this.bOND_OKField;
        }
        set
        {
            this.bOND_OKField = value;
        }
    }

    /// <remarks/>
    public string CF_LIABILITY_INSURANCE
    {
        get
        {
            return this.cF_LIABILITY_INSURANCEField;
        }
        set
        {
            this.cF_LIABILITY_INSURANCEField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_VIOL_CAT_TYPE_DESC
{

    private string iNSP_VIOL_CAT_TYPE_DESC1Field;

    private byte sEQ_ORDER1Field;

    private RPT_REG_CARRIER_PROFILE_XMLG_VIOL_CAT_TYPE_DESCG_VIOL_CAT_TYPE_NAME[] g_VIOL_CAT_TYPE_NAMEField;

    /// <remarks/>
    public string INSP_VIOL_CAT_TYPE_DESC1
    {
        get
        {
            return this.iNSP_VIOL_CAT_TYPE_DESC1Field;
        }
        set
        {
            this.iNSP_VIOL_CAT_TYPE_DESC1Field = value;
        }
    }

    /// <remarks/>
    public byte SEQ_ORDER1
    {
        get
        {
            return this.sEQ_ORDER1Field;
        }
        set
        {
            this.sEQ_ORDER1Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("G_VIOL_CAT_TYPE_NAME")]
    public RPT_REG_CARRIER_PROFILE_XMLG_VIOL_CAT_TYPE_DESCG_VIOL_CAT_TYPE_NAME[] G_VIOL_CAT_TYPE_NAME
    {
        get
        {
            return this.g_VIOL_CAT_TYPE_NAMEField;
        }
        set
        {
            this.g_VIOL_CAT_TYPE_NAMEField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_VIOL_CAT_TYPE_DESCG_VIOL_CAT_TYPE_NAME
{

    private string iNSP_VIOLATION_CATEGORY_NAME1Field;

    private string iNSP_VIOLATION_CATEGORY_DESCField;

    /// <remarks/>
    public string INSP_VIOLATION_CATEGORY_NAME1
    {
        get
        {
            return this.iNSP_VIOLATION_CATEGORY_NAME1Field;
        }
        set
        {
            this.iNSP_VIOLATION_CATEGORY_NAME1Field = value;
        }
    }

    /// <remarks/>
    public string INSP_VIOLATION_CATEGORY_DESC
    {
        get
        {
            return this.iNSP_VIOLATION_CATEGORY_DESCField;
        }
        set
        {
            this.iNSP_VIOLATION_CATEGORY_DESCField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP_VIOL
{

    private string tRANS_ID11Field;

    private uint dOT_NUMBER11Field;

    private RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP_VIOLG_INSP_YEAR1[] g_INSP_YEAR1Field;

    /// <remarks/>
    public string TRANS_ID11
    {
        get
        {
            return this.tRANS_ID11Field;
        }
        set
        {
            this.tRANS_ID11Field = value;
        }
    }

    /// <remarks/>
    public uint DOT_NUMBER11
    {
        get
        {
            return this.dOT_NUMBER11Field;
        }
        set
        {
            this.dOT_NUMBER11Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("G_INSP_YEAR1")]
    public RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP_VIOLG_INSP_YEAR1[] G_INSP_YEAR1
    {
        get
        {
            return this.g_INSP_YEAR1Field;
        }
        set
        {
            this.g_INSP_YEAR1Field = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP_VIOLG_INSP_YEAR1
{

    private string iNSP_YEAR1Field;

    private RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP_VIOLG_INSP_YEAR1G_INSP_VIOL_CAT_TYPE_DESC[] g_INSP_VIOL_CAT_TYPE_DESCField;

    private byte cS_INSP_VIOL_BY_TYPE_TOTALField;

    /// <remarks/>
    public string INSP_YEAR1
    {
        get
        {
            return this.iNSP_YEAR1Field;
        }
        set
        {
            this.iNSP_YEAR1Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("G_INSP_VIOL_CAT_TYPE_DESC")]
    public RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP_VIOLG_INSP_YEAR1G_INSP_VIOL_CAT_TYPE_DESC[] G_INSP_VIOL_CAT_TYPE_DESC
    {
        get
        {
            return this.g_INSP_VIOL_CAT_TYPE_DESCField;
        }
        set
        {
            this.g_INSP_VIOL_CAT_TYPE_DESCField = value;
        }
    }

    /// <remarks/>
    public byte CS_INSP_VIOL_BY_TYPE_TOTAL
    {
        get
        {
            return this.cS_INSP_VIOL_BY_TYPE_TOTALField;
        }
        set
        {
            this.cS_INSP_VIOL_BY_TYPE_TOTALField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP_VIOLG_INSP_YEAR1G_INSP_VIOL_CAT_TYPE_DESC
{

    private byte sEQ_ORDERField;

    private string iNSP_VIOL_CAT_TYPE_DESCField;

    private RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP_VIOLG_INSP_YEAR1G_INSP_VIOL_CAT_TYPE_DESCG_RPT_REG_CP_INSP_VIOL_ID[] g_RPT_REG_CP_INSP_VIOL_IDField;

    /// <remarks/>
    public byte SEQ_ORDER
    {
        get
        {
            return this.sEQ_ORDERField;
        }
        set
        {
            this.sEQ_ORDERField = value;
        }
    }

    /// <remarks/>
    public string INSP_VIOL_CAT_TYPE_DESC
    {
        get
        {
            return this.iNSP_VIOL_CAT_TYPE_DESCField;
        }
        set
        {
            this.iNSP_VIOL_CAT_TYPE_DESCField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("G_RPT_REG_CP_INSP_VIOL_ID")]
    public RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP_VIOLG_INSP_YEAR1G_INSP_VIOL_CAT_TYPE_DESCG_RPT_REG_CP_INSP_VIOL_ID[] G_RPT_REG_CP_INSP_VIOL_ID
    {
        get
        {
            return this.g_RPT_REG_CP_INSP_VIOL_IDField;
        }
        set
        {
            this.g_RPT_REG_CP_INSP_VIOL_IDField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP_VIOLG_INSP_YEAR1G_INSP_VIOL_CAT_TYPE_DESCG_RPT_REG_CP_INSP_VIOL_ID
{

    private string iNSP_VIOLATION_CATEGORY_DESC1Field;

    private byte iNSP_VIOLATION_CATEGORY_CODEField;

    private uint rPT_REG_CP_INSP_VIOL_IDField;

    private byte iNSP_VIOLATION_CATEGORY_IDField;

    private byte cOUNT_VIOLField;

    private string iNSP_VIOLATION_CATEGORY_NAMEField;

    private decimal cF_VIOL_BY_TYPE_PRCField;

    /// <remarks/>
    public string INSP_VIOLATION_CATEGORY_DESC1
    {
        get
        {
            return this.iNSP_VIOLATION_CATEGORY_DESC1Field;
        }
        set
        {
            this.iNSP_VIOLATION_CATEGORY_DESC1Field = value;
        }
    }

    /// <remarks/>
    public byte INSP_VIOLATION_CATEGORY_CODE
    {
        get
        {
            return this.iNSP_VIOLATION_CATEGORY_CODEField;
        }
        set
        {
            this.iNSP_VIOLATION_CATEGORY_CODEField = value;
        }
    }

    /// <remarks/>
    public uint RPT_REG_CP_INSP_VIOL_ID
    {
        get
        {
            return this.rPT_REG_CP_INSP_VIOL_IDField;
        }
        set
        {
            this.rPT_REG_CP_INSP_VIOL_IDField = value;
        }
    }

    /// <remarks/>
    public byte INSP_VIOLATION_CATEGORY_ID
    {
        get
        {
            return this.iNSP_VIOLATION_CATEGORY_IDField;
        }
        set
        {
            this.iNSP_VIOLATION_CATEGORY_IDField = value;
        }
    }

    /// <remarks/>
    public byte COUNT_VIOL
    {
        get
        {
            return this.cOUNT_VIOLField;
        }
        set
        {
            this.cOUNT_VIOLField = value;
        }
    }

    /// <remarks/>
    public string INSP_VIOLATION_CATEGORY_NAME
    {
        get
        {
            return this.iNSP_VIOLATION_CATEGORY_NAMEField;
        }
        set
        {
            this.iNSP_VIOLATION_CATEGORY_NAMEField = value;
        }
    }

    /// <remarks/>
    public decimal CF_VIOL_BY_TYPE_PRC
    {
        get
        {
            return this.cF_VIOL_BY_TYPE_PRCField;
        }
        set
        {
            this.cF_VIOL_BY_TYPE_PRCField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_NOT_PRINTED
{

    private string tRANS_ID13Field;

    private uint dOT_NUMBER14Field;

    private RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_NOT_PRINTEDG_RPT_REG_CP_NOT_PRINTED_ID[] g_RPT_REG_CP_NOT_PRINTED_IDField;

    /// <remarks/>
    public string TRANS_ID13
    {
        get
        {
            return this.tRANS_ID13Field;
        }
        set
        {
            this.tRANS_ID13Field = value;
        }
    }

    /// <remarks/>
    public uint DOT_NUMBER14
    {
        get
        {
            return this.dOT_NUMBER14Field;
        }
        set
        {
            this.dOT_NUMBER14Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("G_RPT_REG_CP_NOT_PRINTED_ID")]
    public RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_NOT_PRINTEDG_RPT_REG_CP_NOT_PRINTED_ID[] G_RPT_REG_CP_NOT_PRINTED_ID
    {
        get
        {
            return this.g_RPT_REG_CP_NOT_PRINTED_IDField;
        }
        set
        {
            this.g_RPT_REG_CP_NOT_PRINTED_IDField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_NOT_PRINTEDG_RPT_REG_CP_NOT_PRINTED_ID
{

    private uint rPT_REG_CP_NOT_PRINTED_IDField;

    private string rEPORTS_NOT_PRINTEDField;

    /// <remarks/>
    public uint RPT_REG_CP_NOT_PRINTED_ID
    {
        get
        {
            return this.rPT_REG_CP_NOT_PRINTED_IDField;
        }
        set
        {
            this.rPT_REG_CP_NOT_PRINTED_IDField = value;
        }
    }

    /// <remarks/>
    public string REPORTS_NOT_PRINTED
    {
        get
        {
            return this.rEPORTS_NOT_PRINTEDField;
        }
        set
        {
            this.rEPORTS_NOT_PRINTEDField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_CRASH
{

    private string tRANS_ID9Field;

    private uint dOT_NUMBER9Field;

    private RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_CRASHG_RPT_REG_CP_CRASH_ID[] g_RPT_REG_CP_CRASH_IDField;

    /// <remarks/>
    public string TRANS_ID9
    {
        get
        {
            return this.tRANS_ID9Field;
        }
        set
        {
            this.tRANS_ID9Field = value;
        }
    }

    /// <remarks/>
    public uint DOT_NUMBER9
    {
        get
        {
            return this.dOT_NUMBER9Field;
        }
        set
        {
            this.dOT_NUMBER9Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("G_RPT_REG_CP_CRASH_ID")]
    public RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_CRASHG_RPT_REG_CP_CRASH_ID[] G_RPT_REG_CP_CRASH_ID
    {
        get
        {
            return this.g_RPT_REG_CP_CRASH_IDField;
        }
        set
        {
            this.g_RPT_REG_CP_CRASH_IDField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_CRASHG_RPT_REG_CP_CRASH_ID
{

    private string sEQ_OF_EVENTS1Field;

    private string sEQ_OF_EVENTS2Field;

    private string sEQ_OF_EVENTS3Field;

    private object sEQ_OF_EVENTS4Field;

    private string dRIVER_LAST_NAMEField;

    private string dRIVER_FIRST_NAMEField;

    private string dRIVER_MIField;

    private string tOW_AWAYField;

    private uint rPT_REG_CP_CRASH_IDField;

    private string rEPORT_DATEField;

    private string rEPORT_TIMEField;

    private string rEPORT_NUMBERField;

    private string lOCATIONField;

    private string cOUNTYField;

    private string cITY1Field;

    private string sTATE1Field;

    private byte fATALITIES1Field;

    private byte iNJURIES1Field;

    private string hAZMAT_FLAGField;

    private string cARRIER_NAMEField;

    private string cARRIER_CITYField;

    private string cARRIER_STATEField;

    private string vEH_LIC_NUMBERField;

    private string vEH_LIC_STATEField;

    private string dRIVER_LIC_NUMBERField;

    private string dRIVER_LIC_STATEField;

    private string dRIVER_BDAYField;

    /// <remarks/>
    public string SEQ_OF_EVENTS1
    {
        get
        {
            return this.sEQ_OF_EVENTS1Field;
        }
        set
        {
            this.sEQ_OF_EVENTS1Field = value;
        }
    }

    /// <remarks/>
    public string SEQ_OF_EVENTS2
    {
        get
        {
            return this.sEQ_OF_EVENTS2Field;
        }
        set
        {
            this.sEQ_OF_EVENTS2Field = value;
        }
    }

    /// <remarks/>
    public string SEQ_OF_EVENTS3
    {
        get
        {
            return this.sEQ_OF_EVENTS3Field;
        }
        set
        {
            this.sEQ_OF_EVENTS3Field = value;
        }
    }

    /// <remarks/>
    public object SEQ_OF_EVENTS4
    {
        get
        {
            return this.sEQ_OF_EVENTS4Field;
        }
        set
        {
            this.sEQ_OF_EVENTS4Field = value;
        }
    }

    /// <remarks/>
    public string DRIVER_LAST_NAME
    {
        get
        {
            return this.dRIVER_LAST_NAMEField;
        }
        set
        {
            this.dRIVER_LAST_NAMEField = value;
        }
    }

    /// <remarks/>
    public string DRIVER_FIRST_NAME
    {
        get
        {
            return this.dRIVER_FIRST_NAMEField;
        }
        set
        {
            this.dRIVER_FIRST_NAMEField = value;
        }
    }

    /// <remarks/>
    public string DRIVER_MI
    {
        get
        {
            return this.dRIVER_MIField;
        }
        set
        {
            this.dRIVER_MIField = value;
        }
    }

    /// <remarks/>
    public string TOW_AWAY
    {
        get
        {
            return this.tOW_AWAYField;
        }
        set
        {
            this.tOW_AWAYField = value;
        }
    }

    /// <remarks/>
    public uint RPT_REG_CP_CRASH_ID
    {
        get
        {
            return this.rPT_REG_CP_CRASH_IDField;
        }
        set
        {
            this.rPT_REG_CP_CRASH_IDField = value;
        }
    }

    /// <remarks/>
    public string REPORT_DATE
    {
        get
        {
            return this.rEPORT_DATEField;
        }
        set
        {
            this.rEPORT_DATEField = value;
        }
    }

    /// <remarks/>
    public string REPORT_TIME
    {
        get
        {
            return this.rEPORT_TIMEField;
        }
        set
        {
            this.rEPORT_TIMEField = value;
        }
    }

    /// <remarks/>
    public string REPORT_NUMBER
    {
        get
        {
            return this.rEPORT_NUMBERField;
        }
        set
        {
            this.rEPORT_NUMBERField = value;
        }
    }

    /// <remarks/>
    public string LOCATION
    {
        get
        {
            return this.lOCATIONField;
        }
        set
        {
            this.lOCATIONField = value;
        }
    }

    /// <remarks/>
    public string COUNTY
    {
        get
        {
            return this.cOUNTYField;
        }
        set
        {
            this.cOUNTYField = value;
        }
    }

    /// <remarks/>
    public string CITY1
    {
        get
        {
            return this.cITY1Field;
        }
        set
        {
            this.cITY1Field = value;
        }
    }

    /// <remarks/>
    public string STATE1
    {
        get
        {
            return this.sTATE1Field;
        }
        set
        {
            this.sTATE1Field = value;
        }
    }

    /// <remarks/>
    public byte FATALITIES1
    {
        get
        {
            return this.fATALITIES1Field;
        }
        set
        {
            this.fATALITIES1Field = value;
        }
    }

    /// <remarks/>
    public byte INJURIES1
    {
        get
        {
            return this.iNJURIES1Field;
        }
        set
        {
            this.iNJURIES1Field = value;
        }
    }

    /// <remarks/>
    public string HAZMAT_FLAG
    {
        get
        {
            return this.hAZMAT_FLAGField;
        }
        set
        {
            this.hAZMAT_FLAGField = value;
        }
    }

    /// <remarks/>
    public string CARRIER_NAME
    {
        get
        {
            return this.cARRIER_NAMEField;
        }
        set
        {
            this.cARRIER_NAMEField = value;
        }
    }

    /// <remarks/>
    public string CARRIER_CITY
    {
        get
        {
            return this.cARRIER_CITYField;
        }
        set
        {
            this.cARRIER_CITYField = value;
        }
    }

    /// <remarks/>
    public string CARRIER_STATE
    {
        get
        {
            return this.cARRIER_STATEField;
        }
        set
        {
            this.cARRIER_STATEField = value;
        }
    }

    /// <remarks/>
    public string VEH_LIC_NUMBER
    {
        get
        {
            return this.vEH_LIC_NUMBERField;
        }
        set
        {
            this.vEH_LIC_NUMBERField = value;
        }
    }

    /// <remarks/>
    public string VEH_LIC_STATE
    {
        get
        {
            return this.vEH_LIC_STATEField;
        }
        set
        {
            this.vEH_LIC_STATEField = value;
        }
    }

    /// <remarks/>
    public string DRIVER_LIC_NUMBER
    {
        get
        {
            return this.dRIVER_LIC_NUMBERField;
        }
        set
        {
            this.dRIVER_LIC_NUMBERField = value;
        }
    }

    /// <remarks/>
    public string DRIVER_LIC_STATE
    {
        get
        {
            return this.dRIVER_LIC_STATEField;
        }
        set
        {
            this.dRIVER_LIC_STATEField = value;
        }
    }

    /// <remarks/>
    public string DRIVER_BDAY
    {
        get
        {
            return this.dRIVER_BDAYField;
        }
        set
        {
            this.dRIVER_BDAYField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_SAFESTAT
{

    private string tRANS_ID6Field;

    private string dOT_NUMBER6Field;

    private RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_SAFESTATG_RPT_REG_CP_SAFESTAT_ID[] g_RPT_REG_CP_SAFESTAT_IDField;

    /// <remarks/>
    public string TRANS_ID6
    {
        get
        {
            return this.tRANS_ID6Field;
        }
        set
        {
            this.tRANS_ID6Field = value;
        }
    }

    /// <remarks/>
    public string DOT_NUMBER6
    {
        get
        {
            return this.dOT_NUMBER6Field;
        }
        set
        {
            this.dOT_NUMBER6Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("G_RPT_REG_CP_SAFESTAT_ID")]
    public RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_SAFESTATG_RPT_REG_CP_SAFESTAT_ID[] G_RPT_REG_CP_SAFESTAT_ID
    {
        get
        {
            return this.g_RPT_REG_CP_SAFESTAT_IDField;
        }
        set
        {
            this.g_RPT_REG_CP_SAFESTAT_IDField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_SAFESTATG_RPT_REG_CP_SAFESTAT_ID
{

    private string sAFESTAT_SCORE_DATEField;

    private object sAFESTAT_SCOREField;

    private decimal aCC_SEAField;

    private decimal dRV_SEAField;

    private decimal vEH_SEAField;

    private byte mGT_SEAField;

    private string sAFESTAT_CATEGORYField;

    private string sAFESTAT_CATEGORY_DESCField;

    private uint rPT_REG_CP_SAFESTAT_IDField;

    /// <remarks/>
    public string SAFESTAT_SCORE_DATE
    {
        get
        {
            return this.sAFESTAT_SCORE_DATEField;
        }
        set
        {
            this.sAFESTAT_SCORE_DATEField = value;
        }
    }

    /// <remarks/>
    public object SAFESTAT_SCORE
    {
        get
        {
            return this.sAFESTAT_SCOREField;
        }
        set
        {
            this.sAFESTAT_SCOREField = value;
        }
    }

    /// <remarks/>
    public decimal ACC_SEA
    {
        get
        {
            return this.aCC_SEAField;
        }
        set
        {
            this.aCC_SEAField = value;
        }
    }

    /// <remarks/>
    public decimal DRV_SEA
    {
        get
        {
            return this.dRV_SEAField;
        }
        set
        {
            this.dRV_SEAField = value;
        }
    }

    /// <remarks/>
    public decimal VEH_SEA
    {
        get
        {
            return this.vEH_SEAField;
        }
        set
        {
            this.vEH_SEAField = value;
        }
    }

    /// <remarks/>
    public byte MGT_SEA
    {
        get
        {
            return this.mGT_SEAField;
        }
        set
        {
            this.mGT_SEAField = value;
        }
    }

    /// <remarks/>
    public string SAFESTAT_CATEGORY
    {
        get
        {
            return this.sAFESTAT_CATEGORYField;
        }
        set
        {
            this.sAFESTAT_CATEGORYField = value;
        }
    }

    /// <remarks/>
    public string SAFESTAT_CATEGORY_DESC
    {
        get
        {
            return this.sAFESTAT_CATEGORY_DESCField;
        }
        set
        {
            this.sAFESTAT_CATEGORY_DESCField = value;
        }
    }

    /// <remarks/>
    public uint RPT_REG_CP_SAFESTAT_ID
    {
        get
        {
            return this.rPT_REG_CP_SAFESTAT_IDField;
        }
        set
        {
            this.rPT_REG_CP_SAFESTAT_IDField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSP
{

    private string dOT_NUMBER12Field;

    private string tRANS_ID12Field;

    private RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSPG_RPT_REG_CP_INSP_ID[] g_RPT_REG_CP_INSP_IDField;

    /// <remarks/>
    public string DOT_NUMBER12
    {
        get
        {
            return this.dOT_NUMBER12Field;
        }
        set
        {
            this.dOT_NUMBER12Field = value;
        }
    }

    /// <remarks/>
    public string TRANS_ID12
    {
        get
        {
            return this.tRANS_ID12Field;
        }
        set
        {
            this.tRANS_ID12Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("G_RPT_REG_CP_INSP_ID")]
    public RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSPG_RPT_REG_CP_INSP_ID[] G_RPT_REG_CP_INSP_ID
    {
        get
        {
            return this.g_RPT_REG_CP_INSP_IDField;
        }
        set
        {
            this.g_RPT_REG_CP_INSP_IDField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSPG_RPT_REG_CP_INSP_ID
{

    private uint iNSPECTION_IDField;

    private byte uNIT_NUMBER1Field;

    private string uNIT_TYPE1Field;

    private string vEH_ST_LICENSE1Field;

    private string uNIT_VIN1Field;

    private string cOMPANY_NUMBER1Field;

    private string uNIT_NUMBER2Field;

    private string uNIT_TYPE2Field;

    private string vEH_ST_LICENSE2Field;

    private string uNIT_VIN2Field;

    private string cOMPANY_NUMBER2Field;

    private object uNIT_NUMBER3Field;

    private object uNIT_TYPE3Field;

    private object vEH_ST_LICENSE3Field;

    private object uNIT_VIN3Field;

    private object cOMPANY_NUMBER3Field;

    private object uNIT_NUMBER4Field;

    private object uNIT_TYPE4Field;

    private object vEH_ST_LICENSE4Field;

    private object uNIT_VIN4Field;

    private object cOMPANY_NUMBER4Field;

    private object uNIT_NUMBER5Field;

    private object uNIT_TYPE5Field;

    private object vEH_ST_LICENSE5Field;

    private object uNIT_VIN5Field;

    private object cOMPANY_NUMBER5Field;

    private object uNIT_NUMBER6Field;

    private object uNIT_TYPE6Field;

    private object vEH_ST_LICENSE6Field;

    private object uNIT_VIN6Field;

    private object cOMPANY_NUMBER6Field;

    private string dRIVER_LAST_NAMEField;

    private string dRIVER_FIRST_NAME1Field;

    private string dRIVER_MI1Field;

    private byte iNSP_LEVEL_CODEField;

    private string iNSP_DATEField;

    private string iNSP_TIMEField;

    private string rEPORT_STATEField;

    private string rEPORT_NUMBER1Field;

    private string cOUNTY_NAME1Field;

    private string lOCATION1Field;

    private string hmField;

    private byte tOTAL_VIOLField;

    private byte tOTAL_OOSField;

    private string cARRIER_NAME1Field;

    private string cARRIER_CITY1Field;

    private string cARRIER_STATE1Field;

    private string dRIVER_LICENSEField;

    private string dRIVER_LICENSE_STATEField;

    private string dRIVER_DOBField;

    private RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSPG_RPT_REG_CP_INSP_IDG_RPT_REG_CP_INSP_VIOL_DETAIL[] lIST_G_RPT_REG_CP_INSP_VIOL_DETAILField;

    /// <remarks/>
    public uint INSPECTION_ID
    {
        get
        {
            return this.iNSPECTION_IDField;
        }
        set
        {
            this.iNSPECTION_IDField = value;
        }
    }

    /// <remarks/>
    public byte UNIT_NUMBER1
    {
        get
        {
            return this.uNIT_NUMBER1Field;
        }
        set
        {
            this.uNIT_NUMBER1Field = value;
        }
    }

    /// <remarks/>
    public string UNIT_TYPE1
    {
        get
        {
            return this.uNIT_TYPE1Field;
        }
        set
        {
            this.uNIT_TYPE1Field = value;
        }
    }

    /// <remarks/>
    public string VEH_ST_LICENSE1
    {
        get
        {
            return this.vEH_ST_LICENSE1Field;
        }
        set
        {
            this.vEH_ST_LICENSE1Field = value;
        }
    }

    /// <remarks/>
    public string UNIT_VIN1
    {
        get
        {
            return this.uNIT_VIN1Field;
        }
        set
        {
            this.uNIT_VIN1Field = value;
        }
    }

    /// <remarks/>
    public string COMPANY_NUMBER1
    {
        get
        {
            return this.cOMPANY_NUMBER1Field;
        }
        set
        {
            this.cOMPANY_NUMBER1Field = value;
        }
    }

    /// <remarks/>
    public string UNIT_NUMBER2
    {
        get
        {
            return this.uNIT_NUMBER2Field;
        }
        set
        {
            this.uNIT_NUMBER2Field = value;
        }
    }

    /// <remarks/>
    public string UNIT_TYPE2
    {
        get
        {
            return this.uNIT_TYPE2Field;
        }
        set
        {
            this.uNIT_TYPE2Field = value;
        }
    }

    /// <remarks/>
    public string VEH_ST_LICENSE2
    {
        get
        {
            return this.vEH_ST_LICENSE2Field;
        }
        set
        {
            this.vEH_ST_LICENSE2Field = value;
        }
    }

    /// <remarks/>
    public string UNIT_VIN2
    {
        get
        {
            return this.uNIT_VIN2Field;
        }
        set
        {
            this.uNIT_VIN2Field = value;
        }
    }

    /// <remarks/>
    public string COMPANY_NUMBER2
    {
        get
        {
            return this.cOMPANY_NUMBER2Field;
        }
        set
        {
            this.cOMPANY_NUMBER2Field = value;
        }
    }

    /// <remarks/>
    public object UNIT_NUMBER3
    {
        get
        {
            return this.uNIT_NUMBER3Field;
        }
        set
        {
            this.uNIT_NUMBER3Field = value;
        }
    }

    /// <remarks/>
    public object UNIT_TYPE3
    {
        get
        {
            return this.uNIT_TYPE3Field;
        }
        set
        {
            this.uNIT_TYPE3Field = value;
        }
    }

    /// <remarks/>
    public object VEH_ST_LICENSE3
    {
        get
        {
            return this.vEH_ST_LICENSE3Field;
        }
        set
        {
            this.vEH_ST_LICENSE3Field = value;
        }
    }

    /// <remarks/>
    public object UNIT_VIN3
    {
        get
        {
            return this.uNIT_VIN3Field;
        }
        set
        {
            this.uNIT_VIN3Field = value;
        }
    }

    /// <remarks/>
    public object COMPANY_NUMBER3
    {
        get
        {
            return this.cOMPANY_NUMBER3Field;
        }
        set
        {
            this.cOMPANY_NUMBER3Field = value;
        }
    }

    /// <remarks/>
    public object UNIT_NUMBER4
    {
        get
        {
            return this.uNIT_NUMBER4Field;
        }
        set
        {
            this.uNIT_NUMBER4Field = value;
        }
    }

    /// <remarks/>
    public object UNIT_TYPE4
    {
        get
        {
            return this.uNIT_TYPE4Field;
        }
        set
        {
            this.uNIT_TYPE4Field = value;
        }
    }

    /// <remarks/>
    public object VEH_ST_LICENSE4
    {
        get
        {
            return this.vEH_ST_LICENSE4Field;
        }
        set
        {
            this.vEH_ST_LICENSE4Field = value;
        }
    }

    /// <remarks/>
    public object UNIT_VIN4
    {
        get
        {
            return this.uNIT_VIN4Field;
        }
        set
        {
            this.uNIT_VIN4Field = value;
        }
    }

    /// <remarks/>
    public object COMPANY_NUMBER4
    {
        get
        {
            return this.cOMPANY_NUMBER4Field;
        }
        set
        {
            this.cOMPANY_NUMBER4Field = value;
        }
    }

    /// <remarks/>
    public object UNIT_NUMBER5
    {
        get
        {
            return this.uNIT_NUMBER5Field;
        }
        set
        {
            this.uNIT_NUMBER5Field = value;
        }
    }

    /// <remarks/>
    public object UNIT_TYPE5
    {
        get
        {
            return this.uNIT_TYPE5Field;
        }
        set
        {
            this.uNIT_TYPE5Field = value;
        }
    }

    /// <remarks/>
    public object VEH_ST_LICENSE5
    {
        get
        {
            return this.vEH_ST_LICENSE5Field;
        }
        set
        {
            this.vEH_ST_LICENSE5Field = value;
        }
    }

    /// <remarks/>
    public object UNIT_VIN5
    {
        get
        {
            return this.uNIT_VIN5Field;
        }
        set
        {
            this.uNIT_VIN5Field = value;
        }
    }

    /// <remarks/>
    public object COMPANY_NUMBER5
    {
        get
        {
            return this.cOMPANY_NUMBER5Field;
        }
        set
        {
            this.cOMPANY_NUMBER5Field = value;
        }
    }

    /// <remarks/>
    public object UNIT_NUMBER6
    {
        get
        {
            return this.uNIT_NUMBER6Field;
        }
        set
        {
            this.uNIT_NUMBER6Field = value;
        }
    }

    /// <remarks/>
    public object UNIT_TYPE6
    {
        get
        {
            return this.uNIT_TYPE6Field;
        }
        set
        {
            this.uNIT_TYPE6Field = value;
        }
    }

    /// <remarks/>
    public object VEH_ST_LICENSE6
    {
        get
        {
            return this.vEH_ST_LICENSE6Field;
        }
        set
        {
            this.vEH_ST_LICENSE6Field = value;
        }
    }

    /// <remarks/>
    public object UNIT_VIN6
    {
        get
        {
            return this.uNIT_VIN6Field;
        }
        set
        {
            this.uNIT_VIN6Field = value;
        }
    }

    /// <remarks/>
    public object COMPANY_NUMBER6
    {
        get
        {
            return this.cOMPANY_NUMBER6Field;
        }
        set
        {
            this.cOMPANY_NUMBER6Field = value;
        }
    }

    /// <remarks/>
    public string DRIVER_LAST_NAME
    {
        get
        {
            return this.dRIVER_LAST_NAMEField;
        }
        set
        {
            this.dRIVER_LAST_NAMEField = value;
        }
    }

    /// <remarks/>
    public string DRIVER_FIRST_NAME1
    {
        get
        {
            return this.dRIVER_FIRST_NAME1Field;
        }
        set
        {
            this.dRIVER_FIRST_NAME1Field = value;
        }
    }

    /// <remarks/>
    public string DRIVER_MI1
    {
        get
        {
            return this.dRIVER_MI1Field;
        }
        set
        {
            this.dRIVER_MI1Field = value;
        }
    }

    /// <remarks/>
    public byte INSP_LEVEL_CODE
    {
        get
        {
            return this.iNSP_LEVEL_CODEField;
        }
        set
        {
            this.iNSP_LEVEL_CODEField = value;
        }
    }

    /// <remarks/>
    public string INSP_DATE
    {
        get
        {
            return this.iNSP_DATEField;
        }
        set
        {
            this.iNSP_DATEField = value;
        }
    }

    /// <remarks/>
    public string INSP_TIME
    {
        get
        {
            return this.iNSP_TIMEField;
        }
        set
        {
            this.iNSP_TIMEField = value;
        }
    }

    /// <remarks/>
    public string REPORT_STATE
    {
        get
        {
            return this.rEPORT_STATEField;
        }
        set
        {
            this.rEPORT_STATEField = value;
        }
    }

    /// <remarks/>
    public string REPORT_NUMBER1
    {
        get
        {
            return this.rEPORT_NUMBER1Field;
        }
        set
        {
            this.rEPORT_NUMBER1Field = value;
        }
    }

    /// <remarks/>
    public string COUNTY_NAME1
    {
        get
        {
            return this.cOUNTY_NAME1Field;
        }
        set
        {
            this.cOUNTY_NAME1Field = value;
        }
    }

    /// <remarks/>
    public string LOCATION1
    {
        get
        {
            return this.lOCATION1Field;
        }
        set
        {
            this.lOCATION1Field = value;
        }
    }

    /// <remarks/>
    public string HM
    {
        get
        {
            return this.hmField;
        }
        set
        {
            this.hmField = value;
        }
    }

    /// <remarks/>
    public byte TOTAL_VIOL
    {
        get
        {
            return this.tOTAL_VIOLField;
        }
        set
        {
            this.tOTAL_VIOLField = value;
        }
    }

    /// <remarks/>
    public byte TOTAL_OOS
    {
        get
        {
            return this.tOTAL_OOSField;
        }
        set
        {
            this.tOTAL_OOSField = value;
        }
    }

    /// <remarks/>
    public string CARRIER_NAME1
    {
        get
        {
            return this.cARRIER_NAME1Field;
        }
        set
        {
            this.cARRIER_NAME1Field = value;
        }
    }

    /// <remarks/>
    public string CARRIER_CITY1
    {
        get
        {
            return this.cARRIER_CITY1Field;
        }
        set
        {
            this.cARRIER_CITY1Field = value;
        }
    }

    /// <remarks/>
    public string CARRIER_STATE1
    {
        get
        {
            return this.cARRIER_STATE1Field;
        }
        set
        {
            this.cARRIER_STATE1Field = value;
        }
    }

    /// <remarks/>
    public string DRIVER_LICENSE
    {
        get
        {
            return this.dRIVER_LICENSEField;
        }
        set
        {
            this.dRIVER_LICENSEField = value;
        }
    }

    /// <remarks/>
    public string DRIVER_LICENSE_STATE
    {
        get
        {
            return this.dRIVER_LICENSE_STATEField;
        }
        set
        {
            this.dRIVER_LICENSE_STATEField = value;
        }
    }

    /// <remarks/>
    public string DRIVER_DOB
    {
        get
        {
            return this.dRIVER_DOBField;
        }
        set
        {
            this.dRIVER_DOBField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("G_RPT_REG_CP_INSP_VIOL_DETAIL", IsNullable = false)]
    public RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSPG_RPT_REG_CP_INSP_IDG_RPT_REG_CP_INSP_VIOL_DETAIL[] LIST_G_RPT_REG_CP_INSP_VIOL_DETAIL
    {
        get
        {
            return this.lIST_G_RPT_REG_CP_INSP_VIOL_DETAILField;
        }
        set
        {
            this.lIST_G_RPT_REG_CP_INSP_VIOL_DETAILField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_RPT_REG_CP_INSPG_RPT_REG_CP_INSP_IDG_RPT_REG_CP_INSP_VIOL_DETAIL
{

    private string vIOL_UNITField;

    private string vIOL_CATField;

    private string vIOL_OOSField;

    private string pOST_CRASH_FLAGField;

    private uint iNSPECTION_ID1Field;

    /// <remarks/>
    public string VIOL_UNIT
    {
        get
        {
            return this.vIOL_UNITField;
        }
        set
        {
            this.vIOL_UNITField = value;
        }
    }

    /// <remarks/>
    public string VIOL_CAT
    {
        get
        {
            return this.vIOL_CATField;
        }
        set
        {
            this.vIOL_CATField = value;
        }
    }

    /// <remarks/>
    public string VIOL_OOS
    {
        get
        {
            return this.vIOL_OOSField;
        }
        set
        {
            this.vIOL_OOSField = value;
        }
    }

    /// <remarks/>
    public string POST_CRASH_FLAG
    {
        get
        {
            return this.pOST_CRASH_FLAGField;
        }
        set
        {
            this.pOST_CRASH_FLAGField = value;
        }
    }

    /// <remarks/>
    public uint INSPECTION_ID1
    {
        get
        {
            return this.iNSPECTION_ID1Field;
        }
        set
        {
            this.iNSPECTION_ID1Field = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_SEQ_OF_EVENTS_DESC
{

    private byte eVENT_CODEField;

    private byte eVENT_IDField;

    private string eVENT_CODE_DESCField;

    private string eVENT_SHORT_NAMEField;

    /// <remarks/>
    public byte EVENT_CODE
    {
        get
        {
            return this.eVENT_CODEField;
        }
        set
        {
            this.eVENT_CODEField = value;
        }
    }

    /// <remarks/>
    public byte EVENT_ID
    {
        get
        {
            return this.eVENT_IDField;
        }
        set
        {
            this.eVENT_IDField = value;
        }
    }

    /// <remarks/>
    public string EVENT_CODE_DESC
    {
        get
        {
            return this.eVENT_CODE_DESCField;
        }
        set
        {
            this.eVENT_CODE_DESCField = value;
        }
    }

    /// <remarks/>
    public string EVENT_SHORT_NAME
    {
        get
        {
            return this.eVENT_SHORT_NAMEField;
        }
        set
        {
            this.eVENT_SHORT_NAMEField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class RPT_REG_CARRIER_PROFILE_XMLG_STATE_CODE
{

    private string sTATE_CODEField;

    private string sTATE_NAMEField;

    private string cONTACT_PHONEField;

    /// <remarks/>
    public string STATE_CODE
    {
        get
        {
            return this.sTATE_CODEField;
        }
        set
        {
            this.sTATE_CODEField = value;
        }
    }

    /// <remarks/>
    public string STATE_NAME
    {
        get
        {
            return this.sTATE_NAMEField;
        }
        set
        {
            this.sTATE_NAMEField = value;
        }
    }

    /// <remarks/>
    public string CONTACT_PHONE
    {
        get
        {
            return this.cONTACT_PHONEField;
        }
        set
        {
            this.cONTACT_PHONEField = value;
        }
    }
}
