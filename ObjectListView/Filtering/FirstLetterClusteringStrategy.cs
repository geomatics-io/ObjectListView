/*
 * FirstLetterClusteringStrategy - A strategy to cluster objects by the first letter of a value
 *
 * Author: Phillip Piper
 * Date: 4-March-2011 11:59 pm
 *
 * Change log:
 * 2011-03-04  JPP  - First version
 * 
 * Copyright (C) 2011 Phillip Piper
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 * If you wish to use this code in a closed source application, please contact phillip_piper@bigfoot.com.
 */

namespace BrightIdeasSoftware {

    /// <summary>
    /// This class implements a strategy where the model objects are clustered
    /// according to the first letter of the string representation of the value
    /// in the configured column.
    /// </summary>
    public class FirstLetterClusteringStrategy : ClusteringStrategy {
        #region Life and death

        /// <summary>
        /// Create a strategy based around the values of the given column
        /// </summary>
        /// <param name="column"></param>
        public FirstLetterClusteringStrategy(OLVColumn column)
            : base(column) {
        }

        #endregion

        #region IClusterStrategy implementation

        /// <summary>
        /// Get the cluster key by which the given model will be partitioned by this strategy
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override object GetClusterKey(object model) {
            object value = this.Column.GetValue(model);
            if (value == null)
                return null;

            string valueAsString = this.Column.ValueToString(value);
            return valueAsString.Length == 0 ? EMPTY_LABEL : valueAsString.Substring(0, 1);
        }

        /// <summary>
        /// Gets the display label that the given cluster should use
        /// </summary>
        /// <param name="cluster"></param>
        /// <returns></returns>
        public override string GetClusterDisplayLabel(ICluster cluster) {
            return this.ApplyDisplayFormat(cluster, cluster.ClusterKey as string);
        }

        #endregion
    }
}
