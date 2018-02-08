using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class CanvasColumn
    {
        #region variables
        public const string CanvasControlAttribute = "data-sp-canvascontrol";
        public const string CanvasDataVersionAttribute = "data-sp-canvasdataversion";
        public const string ControlDataAttribute = "data-sp-controldata";

        private int columnFactor;
        private CanvasSection section;
        private string DataVersion = "1.0";
        #endregion

        // internal constructors as we don't want users to manually create sections
        #region construction
        internal CanvasColumn(CanvasSection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException("Passed section cannot be null");
            }

            this.section = section;
            this.columnFactor = 12;
            this.Order = 0;
        }

        internal CanvasColumn(CanvasSection section, int order)
        {
            if (section == null)
            {
                throw new ArgumentNullException("Passed section cannot be null");
            }

            this.section = section;
            this.Order = order;
        }

        internal CanvasColumn(CanvasSection section, int order, int? sectionFactor)
        {
            if (section == null)
            {
                throw new ArgumentNullException("Passed section cannot be null");
            }

            this.section = section;
            this.Order = order;
            // if the sectionFactor was undefined is was not defined as there was no section in the original markup. Since we however provision back as one column page let's set the sectionFactor to 12.
            this.columnFactor = sectionFactor.HasValue ? sectionFactor.Value : 12;
        }
        #endregion

        #region Properties
        internal int Order { get; set; }

        /// <summary>
        /// <see cref="CanvasSection"/> this section belongs to
        /// </summary>
        public CanvasSection Section
        {
            get
            {
                return this.section;
            }
        }

        /// <summary>
        /// Column size factor. Max value is 12 (= one column), other options are 8,6,4 or 0
        /// </summary>
        public int ColumnFactor
        {
            get
            {
                return this.columnFactor;
            }
        }

        /// <summary>
        /// List of <see cref="CanvasControl"/> instances that are hosted in this section
        /// </summary>
        public System.Collections.Generic.List<CanvasControl> Controls
        {
            get
            {
                return this.Section.Page.Controls.Where(p => p.Section == this.Section && p.Column == this).ToList<CanvasControl>();
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// Renders a HTML presentation of this section
        /// </summary>
        /// <returns>The HTML presentation of this section</returns>
        public string ToHtml()
        {
            StringBuilder html = new StringBuilder(100);

            bool controlWrittenToSection = false;
            int controlIndex = 0;
            foreach (var control in this.Section.Page.Controls.Where(p => p.Section == this.Section && p.Column == this).OrderBy(z => z.Order))
            {
                controlIndex++;
                html.Append(control.ToHtml(controlIndex));
                controlWrittenToSection = true;
            }

            // if a section does not contain a control we still need to render it, otherwise it get's "lost"
            if (!controlWrittenToSection)
            {
                // Obtain the json data
                var clientSideCanvasPosition = new ClientSideCanvasData()
                {
                    Position = new ClientSideCanvasPosition()
                    {
                        ZoneIndex = this.Section.Order,
                        SectionIndex = this.Order,
                        SectionFactor = this.ColumnFactor,
                    }
                };

                var jsonControlData = JsonConvert.SerializeObject(clientSideCanvasPosition);

                html.Append($@"<div {CanvasControlAttribute}="""" {CanvasDataVersionAttribute}=""{this.DataVersion}"" {ControlDataAttribute}=""{jsonControlData}""></div>");
            }

            return html.ToString();
        }
        #endregion
    }
}