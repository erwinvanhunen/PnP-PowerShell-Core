using System.Collections.Generic;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class Field
    {
        public bool AutoIndexed { get; set; }
        public bool CanBeDeleted { get; set; }
        public string ClientSideComponentId { get; set; }
        public object ClientSideComponentProperties { get; set; }
        public object CustomFormatter { get; set; }
        public object DefaultFormula { get; set; }
        public string DefaultValue { get; set; }
        public string Description { get; set; }
        public string Direction { get; set; }
        public bool EnforceUniqueValues { get; set; }
        public string EntityPropertyName { get; set; }
        public bool Filterable { get; set; }
        public bool FromBaseType { get; set; }
        public string Group { get; set; }
        public bool Hidden { get; set; }
        public string Id { get; set; }
        public bool Indexed { get; set; }
        public string InternalName { get; set; }
        public string JSLink { get; set; }
        public bool PinnedToFiltersPane { get; set; }
        public bool ReadOnlyField { get; set; }
        public bool Required { get; set; }
        public string SchemaXml { get; set; }
        public string Scope { get; set; }
        public bool Sealed { get; set; }
        public int ShowInFiltersPane { get; set; }
        public bool Sortable { get; set; }
        public string StaticName { get; set; }
        public string Title { get; set; }
        public int FieldTypeKind { get; set; }
        public string TypeAsString { get; set; }
        public string TypeDisplayName { get; set; }
        public string TypeShortDescription { get; set; }
        public object ValidationFormula { get; set; }
        public object ValidationMessage { get; set; }
        public int MaxLength { get; set; }
        public bool? FillInChoice { get; set; }
        public string Mappings { get; set; }
        public List<object> Choices { get; set; }
        public int? DisplayFormat { get; set; }
        public bool? AllowMultipleValues { get; set; }
        public List<object> DependentLookupInternalNames { get; set; }
        public bool? IsDependentLookup { get; set; }
        public bool? IsRelationship { get; set; }
        public string LookupField { get; set; }
        public string LookupList { get; set; }
        public string LookupWebId { get; set; }
        public string PrimaryFieldId { get; set; }
        public int? RelationshipDeleteBehavior { get; set; }
        public bool? UnlimitedLengthInDocumentLibrary { get; set; }
        public string AnchorId { get; set; }
        public bool? CreateValuesInEditForm { get; set; }
        public bool? IsAnchorValid { get; set; }
        public bool? IsKeyword { get; set; }
        public bool? IsPathRendered { get; set; }
        public bool? IsTermSetValid { get; set; }
        public bool? Open { get; set; }
        public string SspId { get; set; }
        public object TargetTemplate { get; set; }
        public string TermSetId { get; set; }
        public string TextField { get; set; }
        public bool? UserCreated { get; set; }
        public double? MaximumValue { get; set; }
        public double? MinimumValue { get; set; }
        public bool? ShowAsPercentage { get; set; }
        public int? DateTimeCalendarType { get; set; }
        public int? FriendlyDisplayFormat { get; set; }
        public int? EditFormat { get; set; }
        public bool? AllowDisplay { get; set; }
        public bool? Presence { get; set; }
        public int? SelectionGroup { get; set; }
        public int? SelectionMode { get; set; }
        public bool? EnableLookup { get; set; }
        public bool? AllowHyperlink { get; set; }
        public bool? AppendOnly { get; set; }
        public int? NumberOfLines { get; set; }
        public bool? RestrictedMode { get; set; }
        public bool? RichText { get; set; }
        public bool? WikiLinking { get; set; }
        public int? CurrencyLocaleId { get; set; }
        public int? DateFormat { get; set; }
        public string Formula { get; set; }
        public int? OutputType { get; set; }
    }
}