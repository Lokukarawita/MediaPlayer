// Decompiled with JetBrains decompiler
// Type: MediaPlayerFinal.NewDataSet
// Assembly: MediaPlayerFinal, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: FA266A67-626E-46F0-81D3-A2E3F3CE1F98
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\MediaPlayerFinal.exe

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MediaPlayerFinal
{
  [ToolboxItem(true)]
  [XmlRoot("NewDataSet")]
  [DesignerCategory("code")]
  [XmlSchemaProvider("GetTypedDataSetSchema")]
  [HelpKeyword("vs.data.DataSet")]
  [Serializable]
  public class NewDataSet : DataSet
  {
    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    private NewDataSet.songsDataTable tablesongs;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public NewDataSet()
    {
      this.BeginInit();
      this.InitClass();
      CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += changeEventHandler;
      base.Relations.CollectionChanged += changeEventHandler;
      this.EndInit();
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected NewDataSet(SerializationInfo info, StreamingContext context)
      : base(info, context, false)
    {
      if (this.IsBinarySerialized(info, context))
      {
        this.InitVars(false);
        CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
        this.Tables.CollectionChanged += changeEventHandler;
        this.Relations.CollectionChanged += changeEventHandler;
      }
      else
      {
        string s = (string) info.GetValue("XmlSchema", typeof (string));
        if (this.DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.IncludeSchema)
        {
          DataSet dataSet = new DataSet();
          dataSet.ReadXmlSchema((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
          if (dataSet.Tables[nameof (songs)] != null)
            base.Tables.Add((DataTable) new NewDataSet.songsDataTable(dataSet.Tables[nameof (songs)]));
          this.DataSetName = dataSet.DataSetName;
          this.Prefix = dataSet.Prefix;
          this.Namespace = dataSet.Namespace;
          this.Locale = dataSet.Locale;
          this.CaseSensitive = dataSet.CaseSensitive;
          this.EnforceConstraints = dataSet.EnforceConstraints;
          this.Merge(dataSet, false, MissingSchemaAction.Add);
          this.InitVars();
        }
        else
          this.ReadXmlSchema((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
        this.GetSerializationData(info, context);
        CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
        base.Tables.CollectionChanged += changeEventHandler;
        this.Relations.CollectionChanged += changeEventHandler;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public NewDataSet.songsDataTable songs
    {
      get
      {
        return this.tablesongs;
      }
    }

    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [Browsable(true)]
    public override SchemaSerializationMode SchemaSerializationMode
    {
      get
      {
        return this._schemaSerializationMode;
      }
      set
      {
        this._schemaSerializationMode = value;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataTableCollection Tables
    {
      get
      {
        return base.Tables;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataRelationCollection Relations
    {
      get
      {
        return base.Relations;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override void InitializeDerivedDataSet()
    {
      this.BeginInit();
      this.InitClass();
      this.EndInit();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public override DataSet Clone()
    {
      NewDataSet newDataSet = (NewDataSet) base.Clone();
      newDataSet.InitVars();
      newDataSet.SchemaSerializationMode = this.SchemaSerializationMode;
      return (DataSet) newDataSet;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override bool ShouldSerializeTables()
    {
      return false;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override bool ShouldSerializeRelations()
    {
      return false;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override void ReadXmlSerializable(XmlReader reader)
    {
      if (this.DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
      {
        this.Reset();
        DataSet dataSet = new DataSet();
        int num = (int) dataSet.ReadXml(reader);
        if (dataSet.Tables["songs"] != null)
          base.Tables.Add((DataTable) new NewDataSet.songsDataTable(dataSet.Tables["songs"]));
        this.DataSetName = dataSet.DataSetName;
        this.Prefix = dataSet.Prefix;
        this.Namespace = dataSet.Namespace;
        this.Locale = dataSet.Locale;
        this.CaseSensitive = dataSet.CaseSensitive;
        this.EnforceConstraints = dataSet.EnforceConstraints;
        this.Merge(dataSet, false, MissingSchemaAction.Add);
        this.InitVars();
      }
      else
      {
        int num = (int) this.ReadXml(reader);
        this.InitVars();
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override XmlSchema GetSchemaSerializable()
    {
      MemoryStream memoryStream = new MemoryStream();
      this.WriteXmlSchema((XmlWriter) new XmlTextWriter((Stream) memoryStream, (Encoding) null));
      memoryStream.Position = 0L;
      return XmlSchema.Read((XmlReader) new XmlTextReader((Stream) memoryStream), (ValidationEventHandler) null);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal void InitVars()
    {
      this.InitVars(true);
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    internal void InitVars(bool initTable)
    {
      this.tablesongs = (NewDataSet.songsDataTable) base.Tables["songs"];
      if (!initTable || this.tablesongs == null)
        return;
      this.tablesongs.InitVars();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void InitClass()
    {
      this.DataSetName = nameof (NewDataSet);
      this.Prefix = "";
      this.EnforceConstraints = true;
      this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.tablesongs = new NewDataSet.songsDataTable();
      base.Tables.Add((DataTable) this.tablesongs);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private bool ShouldSerializesongs()
    {
      return false;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    private void SchemaChanged(object sender, CollectionChangeEventArgs e)
    {
      if (e.Action != CollectionChangeAction.Remove)
        return;
      this.InitVars();
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
    {
      NewDataSet newDataSet = new NewDataSet();
      XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
      {
        Namespace = newDataSet.Namespace
      });
      schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = newDataSet.GetSchemaSerializable();
      if (xs.Contains(schemaSerializable.TargetNamespace))
      {
        MemoryStream memoryStream1 = new MemoryStream();
        MemoryStream memoryStream2 = new MemoryStream();
        try
        {
          schemaSerializable.Write((Stream) memoryStream1);
          foreach (XmlSchema schema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
          {
            memoryStream2.SetLength(0L);
            schema.Write((Stream) memoryStream2);
            if (memoryStream1.Length == memoryStream2.Length)
            {
              memoryStream1.Position = 0L;
              memoryStream2.Position = 0L;
              do
                ;
              while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
              if (memoryStream1.Position == memoryStream1.Length)
                return schemaComplexType;
            }
          }
        }
        finally
        {
          memoryStream1?.Close();
          memoryStream2?.Close();
        }
      }
      xs.Add(schemaSerializable);
      return schemaComplexType;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public delegate void songsRowChangeEventHandler(object sender, NewDataSet.songsRowChangeEvent e);

    [XmlSchemaProvider("GetTypedTableSchema")]
    [Serializable]
    public class songsDataTable : DataTable, IEnumerable
    {
      private DataColumn columnDisplayView;
      private DataColumn columnsong_ID;
      private DataColumn columnsong_name;
      private DataColumn columnartist;
      private DataColumn columnalbum;
      private DataColumn columnlength;
      private DataColumn columnbit_rate;
      private DataColumn columngenre;
      private DataColumn columnsong_year;
      private DataColumn columndisk;
      private DataColumn columnMIME_type;
      private DataColumn columnlocation;
      private DataColumn columnTimeString;
      private DataColumn column_checked;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public songsDataTable()
      {
        this.TableName = "songs";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      internal songsDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if (table.CaseSensitive != table.DataSet.CaseSensitive)
          this.CaseSensitive = table.CaseSensitive;
        if (table.Locale.ToString() != table.DataSet.Locale.ToString())
          this.Locale = table.Locale;
        if (table.Namespace != table.DataSet.Namespace)
          this.Namespace = table.Namespace;
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected songsDataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn DisplayViewColumn
      {
        get
        {
          return this.columnDisplayView;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn song_IDColumn
      {
        get
        {
          return this.columnsong_ID;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn song_nameColumn
      {
        get
        {
          return this.columnsong_name;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn artistColumn
      {
        get
        {
          return this.columnartist;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn albumColumn
      {
        get
        {
          return this.columnalbum;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn lengthColumn
      {
        get
        {
          return this.columnlength;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn bit_rateColumn
      {
        get
        {
          return this.columnbit_rate;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn genreColumn
      {
        get
        {
          return this.columngenre;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn song_yearColumn
      {
        get
        {
          return this.columnsong_year;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn diskColumn
      {
        get
        {
          return this.columndisk;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn MIME_typeColumn
      {
        get
        {
          return this.columnMIME_type;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn locationColumn
      {
        get
        {
          return this.columnlocation;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn TimeStringColumn
      {
        get
        {
          return this.columnTimeString;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn _checkedColumn
      {
        get
        {
          return this.column_checked;
        }
      }

      [Browsable(false)]
      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public NewDataSet.songsRow this[int index]
      {
        get
        {
          return (NewDataSet.songsRow) this.Rows[index];
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event NewDataSet.songsRowChangeEventHandler songsRowChanging;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event NewDataSet.songsRowChangeEventHandler songsRowChanged;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event NewDataSet.songsRowChangeEventHandler songsRowDeleting;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event NewDataSet.songsRowChangeEventHandler songsRowDeleted;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void AddsongsRow(NewDataSet.songsRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public NewDataSet.songsRow AddsongsRow(
        string DisplayView,
        int song_ID,
        string song_name,
        string artist,
        string album,
        int length,
        int bit_rate,
        string genre,
        string song_year,
        short disk,
        string MIME_type,
        string location,
        string TimeString,
        short _checked)
      {
        NewDataSet.songsRow songsRow = (NewDataSet.songsRow) this.NewRow();
        object[] objArray = new object[14]
        {
          (object) DisplayView,
          (object) song_ID,
          (object) song_name,
          (object) artist,
          (object) album,
          (object) length,
          (object) bit_rate,
          (object) genre,
          (object) song_year,
          (object) disk,
          (object) MIME_type,
          (object) location,
          (object) TimeString,
          (object) _checked
        };
        songsRow.ItemArray = objArray;
        this.Rows.Add((DataRow) songsRow);
        return songsRow;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public virtual IEnumerator GetEnumerator()
      {
        return this.Rows.GetEnumerator();
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public override DataTable Clone()
      {
        NewDataSet.songsDataTable songsDataTable = (NewDataSet.songsDataTable) base.Clone();
        songsDataTable.InitVars();
        return (DataTable) songsDataTable;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new NewDataSet.songsDataTable();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal void InitVars()
      {
        this.columnDisplayView = this.Columns["DisplayView"];
        this.columnsong_ID = this.Columns["song_ID"];
        this.columnsong_name = this.Columns["song_name"];
        this.columnartist = this.Columns["artist"];
        this.columnalbum = this.Columns["album"];
        this.columnlength = this.Columns["length"];
        this.columnbit_rate = this.Columns["bit_rate"];
        this.columngenre = this.Columns["genre"];
        this.columnsong_year = this.Columns["song_year"];
        this.columndisk = this.Columns["disk"];
        this.columnMIME_type = this.Columns["MIME_type"];
        this.columnlocation = this.Columns["location"];
        this.columnTimeString = this.Columns["TimeString"];
        this.column_checked = this.Columns["checked"];
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnDisplayView = new DataColumn("DisplayView", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnDisplayView);
        this.columnsong_ID = new DataColumn("song_ID", typeof (int), (string) null, MappingType.Element);
        this.Columns.Add(this.columnsong_ID);
        this.columnsong_name = new DataColumn("song_name", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnsong_name);
        this.columnartist = new DataColumn("artist", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnartist);
        this.columnalbum = new DataColumn("album", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnalbum);
        this.columnlength = new DataColumn("length", typeof (int), (string) null, MappingType.Element);
        this.Columns.Add(this.columnlength);
        this.columnbit_rate = new DataColumn("bit_rate", typeof (int), (string) null, MappingType.Element);
        this.Columns.Add(this.columnbit_rate);
        this.columngenre = new DataColumn("genre", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columngenre);
        this.columnsong_year = new DataColumn("song_year", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnsong_year);
        this.columndisk = new DataColumn("disk", typeof (short), (string) null, MappingType.Element);
        this.Columns.Add(this.columndisk);
        this.columnMIME_type = new DataColumn("MIME_type", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMIME_type);
        this.columnlocation = new DataColumn("location", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnlocation);
        this.columnTimeString = new DataColumn("TimeString", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnTimeString);
        this.column_checked = new DataColumn("checked", typeof (short), (string) null, MappingType.Element);
        this.Columns.Add(this.column_checked);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public NewDataSet.songsRow NewsongsRow()
      {
        return (NewDataSet.songsRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new NewDataSet.songsRow(builder);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override Type GetRowType()
      {
        return typeof (NewDataSet.songsRow);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.songsRowChanged == null)
          return;
        this.songsRowChanged((object) this, new NewDataSet.songsRowChangeEvent((NewDataSet.songsRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.songsRowChanging == null)
          return;
        this.songsRowChanging((object) this, new NewDataSet.songsRowChangeEvent((NewDataSet.songsRow) e.Row, e.Action));
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.songsRowDeleted == null)
          return;
        this.songsRowDeleted((object) this, new NewDataSet.songsRowChangeEvent((NewDataSet.songsRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.songsRowDeleting == null)
          return;
        this.songsRowDeleting((object) this, new NewDataSet.songsRowChangeEvent((NewDataSet.songsRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void RemovesongsRow(NewDataSet.songsRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        NewDataSet newDataSet = new NewDataSet();
        XmlSchemaAny xmlSchemaAny1 = new XmlSchemaAny();
        xmlSchemaAny1.Namespace = "http://www.w3.org/2001/XMLSchema";
        xmlSchemaAny1.MinOccurs = new Decimal(0);
        xmlSchemaAny1.MaxOccurs = new Decimal(-1, -1, -1, false, (byte) 0);
        xmlSchemaAny1.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny1);
        XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
        xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        xmlSchemaAny2.MinOccurs = new Decimal(1);
        xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny2);
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "namespace",
          FixedValue = newDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = nameof (songsDataTable)
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = newDataSet.GetSchemaSerializable();
        if (xs.Contains(schemaSerializable.TargetNamespace))
        {
          MemoryStream memoryStream1 = new MemoryStream();
          MemoryStream memoryStream2 = new MemoryStream();
          try
          {
            schemaSerializable.Write((Stream) memoryStream1);
            foreach (XmlSchema schema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
            {
              memoryStream2.SetLength(0L);
              schema.Write((Stream) memoryStream2);
              if (memoryStream1.Length == memoryStream2.Length)
              {
                memoryStream1.Position = 0L;
                memoryStream2.Position = 0L;
                do
                  ;
                while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
                if (memoryStream1.Position == memoryStream1.Length)
                  return schemaComplexType;
              }
            }
          }
          finally
          {
            memoryStream1?.Close();
            memoryStream2?.Close();
          }
        }
        xs.Add(schemaSerializable);
        return schemaComplexType;
      }
    }

    public class songsRow : DataRow
    {
      private NewDataSet.songsDataTable tablesongs;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      internal songsRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tablesongs = (NewDataSet.songsDataTable) this.Table;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public string DisplayView
      {
        get
        {
          try
          {
            return (string) this[this.tablesongs.DisplayViewColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'DisplayView' in table 'songs' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tablesongs.DisplayViewColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public int song_ID
      {
        get
        {
          try
          {
            return (int) this[this.tablesongs.song_IDColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'song_ID' in table 'songs' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tablesongs.song_IDColumn] = (object) value;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public string song_name
      {
        get
        {
          try
          {
            return (string) this[this.tablesongs.song_nameColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'song_name' in table 'songs' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tablesongs.song_nameColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string artist
      {
        get
        {
          try
          {
            return (string) this[this.tablesongs.artistColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'artist' in table 'songs' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tablesongs.artistColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string album
      {
        get
        {
          try
          {
            return (string) this[this.tablesongs.albumColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'album' in table 'songs' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tablesongs.albumColumn] = (object) value;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public int length
      {
        get
        {
          try
          {
            return (int) this[this.tablesongs.lengthColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'length' in table 'songs' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tablesongs.lengthColumn] = (object) value;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public int bit_rate
      {
        get
        {
          try
          {
            return (int) this[this.tablesongs.bit_rateColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'bit_rate' in table 'songs' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tablesongs.bit_rateColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string genre
      {
        get
        {
          try
          {
            return (string) this[this.tablesongs.genreColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'genre' in table 'songs' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tablesongs.genreColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string song_year
      {
        get
        {
          try
          {
            return (string) this[this.tablesongs.song_yearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'song_year' in table 'songs' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tablesongs.song_yearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public short disk
      {
        get
        {
          try
          {
            return (short) this[this.tablesongs.diskColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'disk' in table 'songs' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tablesongs.diskColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string MIME_type
      {
        get
        {
          try
          {
            return (string) this[this.tablesongs.MIME_typeColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MIME_type' in table 'songs' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tablesongs.MIME_typeColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string location
      {
        get
        {
          try
          {
            return (string) this[this.tablesongs.locationColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'location' in table 'songs' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tablesongs.locationColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string TimeString
      {
        get
        {
          try
          {
            return (string) this[this.tablesongs.TimeStringColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'TimeString' in table 'songs' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tablesongs.TimeStringColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public short _checked
      {
        get
        {
          try
          {
            return (short) this[this.tablesongs._checkedColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'checked' in table 'songs' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tablesongs._checkedColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsDisplayViewNull()
      {
        return this.IsNull(this.tablesongs.DisplayViewColumn);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void SetDisplayViewNull()
      {
        this[this.tablesongs.DisplayViewColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Issong_IDNull()
      {
        return this.IsNull(this.tablesongs.song_IDColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setsong_IDNull()
      {
        this[this.tablesongs.song_IDColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Issong_nameNull()
      {
        return this.IsNull(this.tablesongs.song_nameColumn);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Setsong_nameNull()
      {
        this[this.tablesongs.song_nameColumn] = Convert.DBNull;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IsartistNull()
      {
        return this.IsNull(this.tablesongs.artistColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetartistNull()
      {
        this[this.tablesongs.artistColumn] = Convert.DBNull;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IsalbumNull()
      {
        return this.IsNull(this.tablesongs.albumColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetalbumNull()
      {
        this[this.tablesongs.albumColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IslengthNull()
      {
        return this.IsNull(this.tablesongs.lengthColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetlengthNull()
      {
        this[this.tablesongs.lengthColumn] = Convert.DBNull;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool Isbit_rateNull()
      {
        return this.IsNull(this.tablesongs.bit_rateColumn);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Setbit_rateNull()
      {
        this[this.tablesongs.bit_rateColumn] = Convert.DBNull;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IsgenreNull()
      {
        return this.IsNull(this.tablesongs.genreColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetgenreNull()
      {
        this[this.tablesongs.genreColumn] = Convert.DBNull;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool Issong_yearNull()
      {
        return this.IsNull(this.tablesongs.song_yearColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setsong_yearNull()
      {
        this[this.tablesongs.song_yearColumn] = Convert.DBNull;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IsdiskNull()
      {
        return this.IsNull(this.tablesongs.diskColumn);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void SetdiskNull()
      {
        this[this.tablesongs.diskColumn] = Convert.DBNull;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IsMIME_typeNull()
      {
        return this.IsNull(this.tablesongs.MIME_typeColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetMIME_typeNull()
      {
        this[this.tablesongs.MIME_typeColumn] = Convert.DBNull;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IslocationNull()
      {
        return this.IsNull(this.tablesongs.locationColumn);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void SetlocationNull()
      {
        this[this.tablesongs.locationColumn] = Convert.DBNull;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IsTimeStringNull()
      {
        return this.IsNull(this.tablesongs.TimeStringColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetTimeStringNull()
      {
        this[this.tablesongs.TimeStringColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Is_checkedNull()
      {
        return this.IsNull(this.tablesongs._checkedColumn);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Set_checkedNull()
      {
        this[this.tablesongs._checkedColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class songsRowChangeEvent : EventArgs
    {
      private NewDataSet.songsRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public songsRowChangeEvent(NewDataSet.songsRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public NewDataSet.songsRow Row
      {
        get
        {
          return this.eventRow;
        }
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }
    }
  }
}
