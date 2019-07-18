using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsJPK_VAT
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://jpk.mf.gov.pl/wzor/2017/11/13/1113/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://jpk.mf.gov.pl/wzor/2017/11/13/1113/", IsNullable = false)]
    ///GETTery i Settery 
    public partial class JPK
    {
        private TNaglowek naglowekField;

        private JPKPodmiot1 podmiot1Field;

        private JPKSprzedazWiersz[] sprzedazWierszField;

        private JPKSprzedazCtrl sprzedazCtrlField;

        private JPKZakupWiersz[] zakupWierszField;

        private JPKZakupCtrl zakupCtrlField;


        //Konstruktor
        public JPK() { }

        //Gettery i Settrery
        /// <remarks/>
        public TNaglowek Naglowek
        {
            get
            {
                return this.naglowekField;
            }
            set
            {
                this.naglowekField = value;
            }
        }//TNaglowek 

        /// <remarks/>
        public JPKPodmiot1 Podmiot1
        {
            get
            {
                return this.podmiot1Field;
            }
            set
            {
                this.podmiot1Field = value;
            }
        }//JPKPodmiot

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SprzedazWiersz")]
        public JPKSprzedazWiersz[] SprzedazWiersz
        {
            get
            {
                return this.sprzedazWierszField;
            }
            set
            {
                this.sprzedazWierszField = value;
            }
        }//JPKSprzedazWiersz[]

        /// <remarks/>
        public JPKSprzedazCtrl SprzedazCtrl
        {
            get
            {
                return this.sprzedazCtrlField;
            }
            set
            {
                this.sprzedazCtrlField = value;
            }
        }//JPKSprzedazCtrl

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ZakupWiersz")]
        public JPKZakupWiersz[] ZakupWiersz
        {
            get
            {
                return this.zakupWierszField;
            }
            set
            {
                this.zakupWierszField = value;
            }
        }//JPKZakupWiersz[]

        /// <remarks/>
        public JPKZakupCtrl ZakupCtrl
        {
            get
            {
                return this.zakupCtrlField;
            }
            set
            {
                this.zakupCtrlField = value;
            }
        }//JPKZakupCtrl


    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://jpk.mf.gov.pl/wzor/2017/11/13/1113/")]
    public partial class TNaglowek
    {

        private TNaglowekKodFormularza kodFormularzaField;

        private sbyte wariantFormularzaField;

        private string celZlozeniaField;

        private System.DateTime dataWytworzeniaJPKField;

        private System.DateTime dataOdField;

        private System.DateTime dataDoField;

        private string nazwaSystemuField;

        public TNaglowek()
        {
            this.celZlozeniaField = "0";
        }

        ///Kod Formularza<remarks/>
        public TNaglowekKodFormularza KodFormularza
        {
            get
            {
                return this.kodFormularzaField;
            }
            set
            {
                this.kodFormularzaField = value;
            }
        }

        /// <remarks/>
        public sbyte WariantFormularza
        {
            get
            {
                return this.wariantFormularzaField;
            }
            set
            {
                this.wariantFormularzaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string CelZlozenia
        {
            get
            {
                return this.celZlozeniaField;
            }
            set
            {
                this.celZlozeniaField = value;
            }
        }

        /// <remarks/>
        public System.DateTime DataWytworzeniaJPK
        {
            get
            {
                return this.dataWytworzeniaJPKField;
            }
            set
            {
                this.dataWytworzeniaJPKField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime DataOd
        {
            get
            {
                return this.dataOdField;
            }
            set
            {
                this.dataOdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime DataDo
        {
            get
            {
                return this.dataDoField;
            }
            set
            {
                this.dataDoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string NazwaSystemu
        {
            get
            {
                return this.nazwaSystemuField;
            }
            set
            {
                this.nazwaSystemuField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://jpk.mf.gov.pl/wzor/2017/11/13/1113/")]
    public partial class TNaglowekKodFormularza
    {

        private string kodSystemowyField;

        private string wersjaSchemyField;

        private TKodFormularza valueField;

        public TNaglowekKodFormularza()
        {
            this.kodSystemowyField = "JPK_VAT (3)";
            this.wersjaSchemyField = "1-1";
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string kodSystemowy
        {
            get
            {
                return this.kodSystemowyField;
            }
            set
            {
                this.kodSystemowyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string wersjaSchemy
        {
            get
            {
                return this.wersjaSchemyField;
            }
            set
            {
                this.wersjaSchemyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public TKodFormularza Value
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://jpk.mf.gov.pl/wzor/2017/11/13/1113/")]
    public enum TKodFormularza
    {

        /// <remarks/>
        JPK_VAT,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://jpk.mf.gov.pl/wzor/2017/11/13/1113/")]
    public partial class JPKPodmiot1
    {

        private string nIPField;

        private string pelnaNazwaField;

        private string emailField;

        /// <remarks/>
        public string NIP
        {
            get
            {
                return this.nIPField;
            }
            set
            {
                this.nIPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string PelnaNazwa
        {
            get
            {
                return this.pelnaNazwaField;
            }
            set
            {
                this.pelnaNazwaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://jpk.mf.gov.pl/wzor/2017/11/13/1113/")]
    public partial class JPKSprzedazWiersz
    {

        private string lpSprzedazyField;

        private string nrKontrahentaField;

        private string nazwaKontrahentaField;

        private string adresKontrahentaField;

        private string dowodSprzedazyField;

        private System.DateTime dataWystawieniaField;

        private System.DateTime dataSprzedazyField;

        private bool dataSprzedazyFieldSpecified;

        private decimal k_10Field;

        private bool k_10FieldSpecified;

        private decimal k_11Field;

        private bool k_11FieldSpecified;

        private decimal k_12Field;

        private bool k_12FieldSpecified;

        private decimal k_13Field;

        private bool k_13FieldSpecified;

        private decimal k_14Field;

        private bool k_14FieldSpecified;

        private decimal k_15Field;

        private decimal k_16Field;

        private decimal k_17Field;

        private decimal k_18Field;

        private decimal k_19Field;

        private decimal k_20Field;

        private decimal k_21Field;

        private bool k_21FieldSpecified;

        private decimal k_22Field;

        private bool k_22FieldSpecified;

        private decimal k_23Field;



        private decimal k_24Field;

        private decimal k_25Field;

        private decimal k_26Field;

        private decimal k_27Field;

        private decimal k_28Field;

        private decimal k_29Field;

        private decimal k_30Field;

        private decimal k_31Field;

        private bool k_31FieldSpecified;

        private decimal k_32Field;

        private decimal k_33Field;

        private decimal k_34Field;

        private decimal k_35Field;

        private decimal k_36Field;

        private bool k_36FieldSpecified;

        private decimal k_37Field;

        private bool k_37FieldSpecified;

        private decimal k_38Field;

        private bool k_38FieldSpecified;

        private decimal k_39Field;

        private bool k_39FieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string LpSprzedazy
        {
            get
            {
                return this.lpSprzedazyField;
            }
            set
            {
                this.lpSprzedazyField = value;
            }
        }

        /// <remarks/>
        public string NrKontrahenta
        {
            get
            {
                return this.nrKontrahentaField;
            }
            set
            {
                this.nrKontrahentaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string NazwaKontrahenta
        {
            get
            {
                return this.nazwaKontrahentaField;
            }
            set
            {
                this.nazwaKontrahentaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string AdresKontrahenta
        {
            get
            {
                return this.adresKontrahentaField;
            }
            set
            {
                this.adresKontrahentaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string DowodSprzedazy
        {
            get
            {
                return this.dowodSprzedazyField;
            }
            set
            {
                this.dowodSprzedazyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime DataWystawienia
        {
            get
            {
                return this.dataWystawieniaField;
            }
            set
            {
                this.dataWystawieniaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime DataSprzedazy
        {
            get
            {
                return this.dataSprzedazyField;
            }
            set
            {
                this.dataSprzedazyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DataSprzedazySpecified
        {
            get
            {
                return this.dataSprzedazyFieldSpecified;
            }
            set
            {
                this.dataSprzedazyFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal K_10
        {
            get
            {
                return this.k_10Field;
            }
            set
            {
                this.k_10Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool K_10Specified
        {
            get
            {
                return this.k_10FieldSpecified;
            }
            set
            {
                this.k_10FieldSpecified = value;
            }
        }


        /// <remarks/>
        public decimal K_11
        {
            get
            {
                return this.k_11Field;
            }
            set
            {
                this.k_11Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool K_11Specified
        {
            get
            {
                return this.k_11FieldSpecified;
            }
            set
            {
                this.k_11FieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal K_12
        {
            get
            {
                return this.k_12Field;
            }
            set
            {
                this.k_12Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool K_12Specified
        {
            get
            {
                return this.k_12FieldSpecified;
            }
            set
            {
                this.k_12FieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal K_13
        {
            get
            {
                return this.k_13Field;
            }
            set
            {
                this.k_13Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool K_13Specified
        {
            get
            {
                return this.k_13FieldSpecified;
            }
            set
            {
                this.k_13FieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal K_14
        {
            get
            {
                return this.k_14Field;
            }
            set
            {
                this.k_14Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool K_14Specified
        {
            get
            {
                return this.k_14FieldSpecified;
            }
            set
            {
                this.k_14FieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal K_15
        {
            get
            {
                return this.k_15Field;
            }
            set
            {
                this.k_15Field = value;
            }
        }

        /// <remarks/>
        public decimal K_16
        {
            get
            {
                return this.k_16Field;
            }
            set
            {
                this.k_16Field = value;
            }
        }

        /// <remarks/>
        public decimal K_17
        {
            get
            {
                return this.k_17Field;
            }
            set
            {
                this.k_17Field = value;
            }
        }

        /// <remarks/>
        public decimal K_18
        {
            get
            {
                return this.k_18Field;
            }
            set
            {
                this.k_18Field = value;
            }
        }

        /// <remarks/>
        public decimal K_19
        {
            get
            {
                return this.k_19Field;
            }
            set
            {
                this.k_19Field = value;
            }
        }

        /// <remarks/>
        public decimal K_20
        {
            get
            {
                return this.k_20Field;
            }
            set
            {
                this.k_20Field = value;
            }
        }

        /// <remarks/>
        public decimal K_21
        {
            get
            {
                return this.k_21Field;
            }
            set
            {
                this.k_21Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool K_21Specified
        {
            get
            {
                return this.k_21FieldSpecified;
            }
            set
            {
                this.k_21FieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal K_22
        {
            get
            {
                return this.k_22Field;
            }
            set
            {
                this.k_22Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool K_22Specified
        {
            get
            {
                return this.k_22FieldSpecified;
            }
            set
            {
                this.k_22FieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal K_23
        {
            get
            {
                return this.k_23Field;
            }
            set
            {
                this.k_23Field = value;
            }
        }

        /// <remarks/>
        public decimal K_24
        {
            get
            {
                return this.k_24Field;
            }
            set
            {
                this.k_24Field = value;
            }
        }

        /// <remarks/>
        public decimal K_25
        {
            get
            {
                return this.k_25Field;
            }
            set
            {
                this.k_25Field = value;
            }
        }

        /// <remarks/>
        public decimal K_26
        {
            get
            {
                return this.k_26Field;
            }
            set
            {
                this.k_26Field = value;
            }
        }

        /// <remarks/>
        public decimal K_27
        {
            get
            {
                return this.k_27Field;
            }
            set
            {
                this.k_27Field = value;
            }
        }

        /// <remarks/>
        public decimal K_28
        {
            get
            {
                return this.k_28Field;
            }
            set
            {
                this.k_28Field = value;
            }
        }

        /// <remarks/>
        public decimal K_29
        {
            get
            {
                return this.k_29Field;
            }
            set
            {
                this.k_29Field = value;
            }
        }

        /// <remarks/>
        public decimal K_30
        {
            get
            {
                return this.k_30Field;
            }
            set
            {
                this.k_30Field = value;
            }
        }

        /// <remarks/>
        public decimal K_31
        {
            get
            {
                return this.k_31Field;
            }
            set
            {
                this.k_31Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool K_31Specified
        {
            get
            {
                return this.k_31FieldSpecified;
            }
            set
            {
                this.k_31FieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal K_32
        {
            get
            {
                return this.k_32Field;
            }
            set
            {
                this.k_32Field = value;
            }
        }

        /// <remarks/>
        public decimal K_33
        {
            get
            {
                return this.k_33Field;
            }
            set
            {
                this.k_33Field = value;
            }
        }

        /// <remarks/>
        public decimal K_34
        {
            get
            {
                return this.k_34Field;
            }
            set
            {
                this.k_34Field = value;
            }
        }

        /// <remarks/>
        public decimal K_35
        {
            get
            {
                return this.k_35Field;
            }
            set
            {
                this.k_35Field = value;
            }
        }

        /// <remarks/>
        public decimal K_36
        {
            get
            {
                return this.k_36Field;
            }
            set
            {
                this.k_36Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool K_36Specified
        {
            get
            {
                return this.k_36FieldSpecified;
            }
            set
            {
                this.k_36FieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal K_37
        {
            get
            {
                return this.k_37Field;
            }
            set
            {
                this.k_37Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool K_37Specified
        {
            get
            {
                return this.k_37FieldSpecified;
            }
            set
            {
                this.k_37FieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal K_38
        {
            get
            {
                return this.k_38Field;
            }
            set
            {
                this.k_38Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool K_38Specified
        {
            get
            {
                return this.k_38FieldSpecified;
            }
            set
            {
                this.k_38FieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal K_39
        {
            get
            {
                return this.k_39Field;
            }
            set
            {
                this.k_39Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool K_39Specified
        {
            get
            {
                return this.k_39FieldSpecified;
            }
            set
            {
                this.k_39FieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://jpk.mf.gov.pl/wzor/2017/11/13/1113/")]
    public partial class JPKSprzedazCtrl
    {

        private string liczbaWierszySprzedazyField;

        private decimal podatekNaleznyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string LiczbaWierszySprzedazy
        {
            get
            {
                return this.liczbaWierszySprzedazyField;
            }
            set
            {
                this.liczbaWierszySprzedazyField = value;
            }
        }

        /// <remarks/>
        public decimal PodatekNalezny
        {
            get
            {
                return this.podatekNaleznyField;
            }
            set
            {
                this.podatekNaleznyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://jpk.mf.gov.pl/wzor/2017/11/13/1113/")]
    public partial class JPKZakupWiersz
    {

        private string lpZakupuField;

        private string nrDostawcyField;

        private string nazwaDostawcyField;

        private string adresDostawcyField;

        private string dowodZakupuField;

        private System.DateTime dataZakupuField;

        private System.DateTime dataWplywuField;

        private bool dataWplywuFieldSpecified;

        private decimal k_43Field;

        private decimal k_44Field;

        private decimal k_45Field;

        private decimal k_46Field;

        private decimal k_47Field;

        private bool k_47FieldSpecified;

        private decimal k_48Field;

        private bool k_48FieldSpecified;

        private decimal k_49Field;

        private bool k_49FieldSpecified;

        private decimal k_50Field;

        private bool k_50FieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string LpZakupu
        {
            get
            {
                return this.lpZakupuField;
            }
            set
            {
                this.lpZakupuField = value;
            }
        }

        /// <remarks/>
        public string NrDostawcy
        {
            get
            {
                return this.nrDostawcyField;
            }
            set
            {
                this.nrDostawcyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string NazwaDostawcy
        {
            get
            {
                return this.nazwaDostawcyField;
            }
            set
            {
                this.nazwaDostawcyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string AdresDostawcy
        {
            get
            {
                return this.adresDostawcyField;
            }
            set
            {
                this.adresDostawcyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token")]
        public string DowodZakupu
        {
            get
            {
                return this.dowodZakupuField;
            }
            set
            {
                this.dowodZakupuField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime DataZakupu
        {
            get
            {
                return this.dataZakupuField;
            }
            set
            {
                this.dataZakupuField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime DataWplywu
        {
            get
            {
                return this.dataWplywuField;
            }
            set
            {
                this.dataWplywuField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DataWplywuSpecified
        {
            get
            {
                return this.dataWplywuFieldSpecified;
            }
            set
            {
                this.dataWplywuFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal K_43
        {
            get
            {
                return this.k_43Field;
            }
            set
            {
                this.k_43Field = value;
            }
        }

        /// <remarks/>
        public decimal K_44
        {
            get
            {
                return this.k_44Field;
            }
            set
            {
                this.k_44Field = value;
            }
        }

        /// <remarks/>
        public decimal K_45
        {
            get
            {
                return this.k_45Field;
            }
            set
            {
                this.k_45Field = value;
            }
        }

        /// <remarks/>
        public decimal K_46
        {
            get
            {
                return this.k_46Field;
            }
            set
            {
                this.k_46Field = value;
            }
        }

        /// <remarks/>
        public decimal K_47
        {
            get
            {
                return this.k_47Field;
            }
            set
            {
                this.k_47Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool K_47Specified
        {
            get
            {
                return this.k_47FieldSpecified;
            }
            set
            {
                this.k_47FieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal K_48
        {
            get
            {
                return this.k_48Field;
            }
            set
            {
                this.k_48Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool K_48Specified
        {
            get
            {
                return this.k_48FieldSpecified;
            }
            set
            {
                this.k_48FieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal K_49
        {
            get
            {
                return this.k_49Field;
            }
            set
            {
                this.k_49Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool K_49Specified
        {
            get
            {
                return this.k_49FieldSpecified;
            }
            set
            {
                this.k_49FieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal K_50
        {
            get
            {
                return this.k_50Field;
            }
            set
            {
                this.k_50Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool K_50Specified
        {
            get
            {
                return this.k_50FieldSpecified;
            }
            set
            {
                this.k_50FieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://jpk.mf.gov.pl/wzor/2017/11/13/1113/")]
    public partial class JPKZakupCtrl
    {

        private string liczbaWierszyZakupowField;

        private decimal podatekNaliczonyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string LiczbaWierszyZakupow
        {
            get
            {
                return this.liczbaWierszyZakupowField;
            }
            set
            {
                this.liczbaWierszyZakupowField = value;
            }
        }

        /// <remarks/>
        public decimal PodatekNaliczony
        {
            get
            {
                return this.podatekNaliczonyField;
            }
            set
            {
                this.podatekNaliczonyField = value;
            }
        }

    }// partial class JPK
}//nsJPK_VAT
