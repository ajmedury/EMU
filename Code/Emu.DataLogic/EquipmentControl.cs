﻿using Emu.Common;
using Emu.DataLogic.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emu.DataLogic
{
    public class EquipmentControl : IEquipmentManager
    {
        #region Properties

        MySqlConnection Connection { get; set; }

        struct SQL
        {
            
            public const string GetAll = @"SELECT 
                                                    BarCode,
                                                    SerialNumber,
                                                    UserID,
                                                    Description,
                                                    Location,
                                                    WarrantyExpiration 
                                            FROM
                                                    Equipment";

            
            public const string GetByBarcode = @"SELECT
                                                            BarCode,
                                                            SerialNumber,
                                                            UserID,
                                                            Description,
                                                            Location,
                                                            WarrantyExpiration
                                                 FROM
                                                            Equipment
                                                 WHERE
                                                            BarCode = @BarCode";
            
            public const string Create = @" INSERT INTO Equipment 
                                            (
                                                BarCode,
                                                SerialNumber,
                                                UserID,
                                                Description,
                                                Location,
                                                WarrantyExpiration
                                            ) 
                                            VALUES
                                            (
                                                @BarCode,
                                                @SerialNumber,
                                                @UserID,
                                                @Description,
                                                @Location,
                                                @WarrantyExpiration
                                            )";

            public const string Update = @" UPDATE
                                                    Equipment 
                                            SET
                                                    SerialNumber = @SerialNumber,
                                                    UserID = @UserID,
                                                    Description = @Description,
                                                    Location = @Location,
                                                    WarrantyExpiration = @WarrantyExpiration
                                            WHERE
                                                    BarCode = @BarCode";
        }

        #endregion
        #region Constructor

        public EquipmentControl()
        {
            Connection = new MySqlConnection( Settings.Default.ConnectionString );
        }

        #endregion
        #region Methods

        public List<Equipment> Get()
        {
            var results = new List<Equipment>();

            Connection.Open();
            using( var cmd = new MySqlCommand( SQL.GetAll, Connection ) )
            {
                using( var reader = cmd.ExecuteReader() )
                {
                    while( reader.Read() )
                    {
                        results.Add( new Equipment
                        {
                            BarCode = Convert.ToInt32( reader[ "BarCode" ].ToString() ),
                            Description = reader[ "Description" ].ToString(),
                            Location = reader[ "Location" ].ToString(),
                            WarrantyExpiration = DateTime.Parse( reader[ "WarrantyExpiration" ].ToString() )
                        } );
                    }
                }
            }
            Connection.Close();

            return results;
        }

        public Equipment Get( int barCode )
        {
            #region Validate Arguments

            if ( barCode.IsPositive() == false )
            {
                throw new ArgumentException( "Barcode argument must be a positive integer.", "barcode" );
            }

            #endregion

            Equipment result = null;

            using( var cmd = new MySqlCommand( SQL.GetByBarcode, Connection ) )
            {
                cmd.Parameters.AddWithValue( "@BarCode", barCode );

                using( var reader = cmd.ExecuteReader() )
                {
                    while( reader.Read() )
                    {
                        result = new Equipment 
                        {
                            BarCode = Convert.ToInt32(reader["BarCode"]),
                            Description = reader["Description"].ToString(),
                            Location = reader["Location"].ToString(),
                            WarrantyExpiration = DateTime.Parse(reader["WarrantyExpiration"].ToString())
                        };

                    }
                }
            }

            // need to load all related lists for Equipment

            return result;
        }

        public void Create( Equipment equipment )
        {
            #region Validate Arguments
            
            if ( equipment == null )
            {
                throw new ArgumentException( "Equipment argument must not be null.", "equipment" );
            }
            if ( equipment.BarCode.IsPositive() == false )
            {
                throw new ArgumentException( "Equipment barcode must be a positive integer.", "barcode" );
            }

            #endregion

            using( var cmd = new MySqlCommand( SQL.Create, Connection ) )
            {
                cmd.Parameters.AddWithValue( "@BarCode", equipment.BarCode );
                cmd.Parameters.AddWithValue( "@Description", equipment.Description );
                cmd.Parameters.AddWithValue( "@Location", equipment.Location );
                cmd.Parameters.AddWithValue( "@WarrantyExpiration", equipment.WarrantyExpiration );

                // run the update statement
                cmd.ExecuteNonQuery();
            }
        }

        public void Update( Equipment equipment )
        {
            #region Validate Arguments

            if ( equipment == null )
            {
                throw new ArgumentException( "Equipment argument must not be null.", "equipment" );
            }
            if ( equipment.BarCode.IsPositive() == false )
            {
                throw new ArgumentException( "Equipment barcode must be a positive integer.", "barcode" );
            }

            #endregion

            using( var cmd = new MySqlCommand( SQL.Update, Connection ) )
            {
                cmd.Parameters.AddWithValue( "@BarCode", equipment.BarCode );
                cmd.Parameters.AddWithValue( "@Description", equipment.Description );
                cmd.Parameters.AddWithValue( "@Location", equipment.Location );
                cmd.Parameters.AddWithValue( "@WarrantyExpiration", equipment.WarrantyExpiration );

                // run the update statement
                cmd.ExecuteNonQuery();
            }
        }

        public void CreateRelationship( Equipment equipment, User user ) 
        {
            #region Validate Arguments

            #endregion
        }

        #endregion
    }
}