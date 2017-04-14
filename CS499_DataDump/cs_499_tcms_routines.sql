CREATE DATABASE  IF NOT EXISTS `cs_499_tcms` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `cs_499_tcms`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: cs_499_tcms
-- ------------------------------------------------------
-- Server version	5.7.17-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Temporary view structure for view `businesspartners_addresses`
--

DROP TABLE IF EXISTS `businesspartners_addresses`;
/*!50001 DROP VIEW IF EXISTS `businesspartners_addresses`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `businesspartners_addresses` AS SELECT 
 1 AS `CompanyID`,
 1 AS `Address`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vehicle_info`
--

DROP TABLE IF EXISTS `vehicle_info`;
/*!50001 DROP VIEW IF EXISTS `vehicle_info`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `vehicle_info` AS SELECT 
 1 AS `VehicleID`,
 1 AS `Vehicle`*/;
SET character_set_client = @saved_cs_client;

--
-- Final view structure for view `businesspartners_addresses`
--

/*!50001 DROP VIEW IF EXISTS `businesspartners_addresses`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`cs499`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `businesspartners_addresses` AS select `businesspartners`.`CompanyID` AS `CompanyID`,concat_ws('\n',convert(`businesspartners`.`CompanyName` using utf8),convert(`businesspartners`.`Address` using utf8),convert(`businesspartners`.`City` using utf8),convert(`businesspartners`.`State` using utf8),`businesspartners`.`ZipCode`,`mask`(cast(`businesspartners`.`PhoneNumber` as unsigned),'(###) ###-####')) AS `Address` from `businesspartners` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vehicle_info`
--

/*!50001 DROP VIEW IF EXISTS `vehicle_info`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`cs499`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `vehicle_info` AS select `vehicle`.`VehicleID` AS `VehicleID`,concat_ws(' ',`vehicle`.`Year`,`vehicle`.`Brand`,`vehicle`.`Model`) AS `Vehicle` from `vehicle` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Dumping routines for database 'cs_499_tcms'
--
/*!50003 DROP FUNCTION IF EXISTS `GenerateBeforeDeleteTrigger` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cs499`@`%` FUNCTION `GenerateBeforeDeleteTrigger`(P_TableName varchar(64), P_SchemaName varchar(64)) RETURNS varchar(4000) CHARSET utf8
BEGIN
	
  -- Declare statement variables
  DECLARE V_StatementDrop varchar(4000);
  DECLARE V_StatementCreate varchar(4000);
  DECLARE V_StatementTrigger varchar(4000);
  DECLARE V_Statement varchar(4000) DEFAULT NULL;
  
  -- Create the drop if exists statement
  SET V_StatementDrop = CONCAT('DROP TRIGGER IF EXISTS ', 
                               P_SchemaName, 
                               '.', 
                               P_TableName, 
                               '_bd; ');
  
  -- Create the create trigger statement
  SET V_StatementCreate = CONCAT('CREATE TRIGGER `', 
                                 P_SchemaName, 
                                 '`.`', 
                                 P_TableName, 
                                 '_bd` BEFORE DELETE ON ', 
                                 P_SchemaName,
                                 '.',
                                 P_TableName,
                                 ' FOR EACH ROW ');
                                 
  -- Create the first part of the trigger body                           
  SET V_StatementTrigger = CONCAT_WS('\n', 
                                     'BEGIN ',                                     
                                     CONCAT('INSERT INTO ', P_TableName, '_log '), 
                                     '( ',
                                     CONCAT_WS(',\n', GetCommaDelimitedTableColNames(P_TableName, P_SchemaName, ''), 'ModifiedStatus', 'DeletedDate '),                                     
                                     ') ',
                                     'VALUES ',
                                     '( ',
                                     CONCAT_WS(',\n', GetCommaDelimitedTableColNames(P_TableName, P_SchemaName, 'OLD.'), '\'D\'', 'CURRENT_TIMESTAMP '),
                                     '); ',
                                     ' ',
                                     'END;');  
                
  SET V_Statement = CONCAT_WS('\n', V_StatementDrop, V_StatementCreate, V_StatementTrigger);  
  
  
	RETURN V_Statement;
  
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `GenerateBeforeInsertTrigger` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cs499`@`%` FUNCTION `GenerateBeforeInsertTrigger`(P_TableName varchar(64), P_SchemaName varchar(64)) RETURNS varchar(4000) CHARSET utf8
BEGIN
	
  DECLARE V_StatementDrop varchar(4000);
  DECLARE V_StatementCreate varchar(4000);
  DECLARE V_StatementTrigger varchar(4000);
  DECLARE V_Statement varchar(4000) DEFAULT NULL;
  
  SET V_StatementDrop = CONCAT('DROP TRIGGER IF EXISTS ', 
                               P_SchemaName, 
                               '.', 
                               P_TableName, 
                               '_bi; ');
  
  SET V_StatementCreate = CONCAT('CREATE TRIGGER `', 
                                 P_SchemaName, 
                                 '`.`', 
                                 P_TableName, 
                                 '_bi` BEFORE INSERT ON ', 
                                 P_SchemaName,
                                 '.',
                                 P_TableName,
                                 ' FOR EACH ROW ');
                             
  SET V_StatementTrigger = CONCAT_WS('\n', 
                                     'BEGIN ',
                                     ' ',
                                     'SET NEW.CREATEDDATE = CURRENT_TIMESTAMP; ',
                                     'SET NEW.VERSION = 1; ',
                                     ' ',
                                     'END;');
  
  SET V_Statement = CONCAT_WS('\n', V_StatementDrop, V_StatementCreate, V_StatementTrigger);  
  
  
	RETURN V_Statement;
  
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `GenerateBeforeUpdateTrigger` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cs499`@`%` FUNCTION `GenerateBeforeUpdateTrigger`(P_TableName varchar(64), P_SchemaName varchar(64)) RETURNS varchar(4000) CHARSET utf8
BEGIN
	
  -- Declare statement variables
  DECLARE V_StatementDrop varchar(4000);
  DECLARE V_StatementCreate varchar(4000);
  DECLARE V_StatementTrigger varchar(4000);
  DECLARE V_Statement varchar(4000) DEFAULT NULL;
  
  -- Create the drop if exists statement
  SET V_StatementDrop = CONCAT('DROP TRIGGER IF EXISTS ', 
                               P_SchemaName, 
                               '.', 
                               P_TableName, 
                               '_bu; ');
  
  -- Create the create trigger statement
  SET V_StatementCreate = CONCAT('CREATE TRIGGER `', 
                                 P_SchemaName, 
                                 '`.`', 
                                 P_TableName, 
                                 '_bu` BEFORE UPDATE ON ', 
                                 P_SchemaName,
                                 '.',
                                 P_TableName,
                                 ' FOR EACH ROW ');
                                 
  -- Create the first part of the trigger body                           
  SET V_StatementTrigger = CONCAT_WS('\n', 
                                     'BEGIN ',
                                     ' ',
                                     'SET NEW.VERSION = OLD.VERSION + 1; ',
                                     'SET NEW.LASTMODIFIEDDATE = CURRENT_TIMESTAMP; ', 
                                     ' ',
                                     CONCAT('INSERT INTO ', P_TableName, '_log '), 
                                     '( ',
                                     CONCAT_WS(',\n', GetCommaDelimitedTableColNames(P_TableName, P_SchemaName, ''), 'ModifiedStatus '),                                     
                                     ') ',
                                     'VALUES ',
                                     '( ',
                                     CONCAT_WS(',\n', GetCommaDelimitedTableColNames(P_TableName, P_SchemaName, 'OLD.'), '\'U\''),
                                     '); ',
                                     ' ',
                                     'END;');  
                
  SET V_Statement = CONCAT_WS('\n', V_StatementDrop, V_StatementCreate, V_StatementTrigger);  
  
  
	RETURN V_Statement;
  
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `GenerateLogTable` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cs499`@`%` FUNCTION `GenerateLogTable`(P_TableName varchar(64), P_SchemaName varchar(64)) RETURNS varchar(4000) CHARSET utf8
BEGIN
	
	-- Declare statement variables
  DECLARE V_StatementDrop varchar(4000);
  DECLARE V_StatementCreate varchar(4000);
  DECLARE V_StatementTable varchar(4000);
  DECLARE V_Statement varchar(4000) DEFAULT NULL;
  
  -- Create the drop if exists statement
  SET V_StatementDrop = CONCAT('DROP TABLE IF EXISTS ', 
                               P_SchemaName, 
                               '.', 
                               P_TableName, 
                               '_log; ');
  
  -- Create the create table statement
  SET V_StatementCreate = CONCAT('CREATE TABLE `', 
                                 P_SchemaName, 
                                 '`.`', 
                                 P_TableName, 
                                 '_log` ( ');
                                 
  -- Create the first part of the table body                           
  SET V_StatementTable = CONCAT_WS('\n', 
                                   GetCommaDelimitedTableColNamesForLogTable(P_TableName, P_SchemaName),                                     
                                   ') ENGINE=InnoDB DEFAULT CHARSET=utf8;');  
                
  SET V_Statement = CONCAT_WS('\n', V_StatementDrop, V_StatementCreate, V_StatementTable);  
  
  
	RETURN V_Statement;
  
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `GetCommaDelimitedTableColNames` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cs499`@`%` FUNCTION `GetCommaDelimitedTableColNames`(P_TableName varchar(64), P_SchemaName varchar(64), P_Prefix varchar(20)) RETURNS varchar(4000) CHARSET utf8
BEGIN
	
  -- Declare column names variable
  DECLARE V_ColNames varchar(4000) DEFAULT "";
  
  -- Concatenate column names
  SET V_ColNames = (SELECT GROUP_CONCAT(DISTINCT CONCAT(P_Prefix, COLUMN_NAME)
                ORDER BY ORDINAL_POSITION SEPARATOR ',\n') COLUMN_NAME
                FROM information_schema.COLUMNS
                WHERE TABLE_SCHEMA = P_SchemaName
                AND TABLE_NAME = P_TableName);
                
	RETURN V_ColNames;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `GetCommaDelimitedTableColNamesForLogTable` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cs499`@`%` FUNCTION `GetCommaDelimitedTableColNamesForLogTable`(P_TableName varchar(64), P_SchemaName varchar(64)) RETURNS varchar(4000) CHARSET utf8
BEGIN
	-- Declare column names variable
  DECLARE V_ColNames varchar(4000) DEFAULT "";
  
  -- Concatenate column names
  SET V_ColNames = (SELECT GROUP_CONCAT(DISTINCT 
                                        CONCAT_WS(' ', CONCAT('`', COLUMN_NAME, '`'),
                                        COLUMN_TYPE,
                                        (CASE DATA_TYPE WHEN 'timestamp' THEN CONCAT('NOT NULL DEFAULT ', '\'', COLUMN_DEFAULT, '\'') ELSE 'DEFAULT NULL' END))
                ORDER BY ORDINAL_POSITION SEPARATOR ',\n') COLUMN_NAME
                FROM information_schema.COLUMNS
                WHERE TABLE_SCHEMA = P_SchemaName
                AND TABLE_NAME = P_TableName);
  
  -- Concatenate log specific column names
  SET V_ColNames = CONCAT_WS(',\n',
                             V_ColNames,
                             '`ModifiedStatus` varchar(1) DEFAULT NULL',
                             '`DeletedDate` timestamp NOT NULL DEFAULT \'0000-00-00 00:00:00\'',
                             '`DeletedBy` varchar(20) DEFAULT NULL');
	RETURN V_ColNames;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `mask` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cs499`@`%` FUNCTION `mask`(unformatted_value BIGINT, format_string CHAR(32)) RETURNS char(32) CHARSET utf8
    DETERMINISTIC
BEGIN
   # Declare variables
   DECLARE input_len    TINYINT;
   DECLARE output_len   TINYINT;
   DECLARE temp_char    CHAR;

   # Initialize variables
   SET input_len = LENGTH(unformatted_value);
   SET output_len = LENGTH(format_string);

   # Construct formated string
   WHILE (output_len > 0)
   DO
      SET temp_char = SUBSTR(format_string, output_len, 1);

      IF (temp_char = '#')
      THEN
         IF (input_len > 0)
         THEN
            SET format_string =
                   INSERT(format_string,
                          output_len,
                          1,
                          SUBSTR(unformatted_value, input_len, 1));
            SET input_len = input_len - 1;
         ELSE
            SET format_string =
                   INSERT(format_string,
                          output_len,
                          1,
                          '0');
         END IF;
      END IF;

      SET output_len = output_len - 1;
   END WHILE;

   RETURN format_string;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteLines` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cs499`@`%` PROCEDURE `DeleteLines`()
BEGIN
	
  DELETE FROM createdstatements;
  
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GenerateAllLogTables` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cs499`@`%` PROCEDURE `GenerateAllLogTables`(P_SchemaName varchar(64))
BEGIN
	
  
  -- Declare return variable
  DECLARE V_Statement varchar(4000) DEFAULT NULL;
  
  -- Declare the done flag variable
  DECLARE done tinyint(1) DEFAULT FALSE;
  
  -- Declare table name variable
  DECLARE V_TableName varchar(64) DEFAULT NULL;
  
  -- Declare cursor and get the table names
  DECLARE CUR_TABLENAMES CURSOR FOR SELECT TABLE_NAME
                                    FROM information_schema.TABLES
                                    WHERE TABLE_SCHEMA = P_SchemaName
                                    AND TABLE_NAME NOT LIKE '%_log';
                                    
  -- Declare empty set flag
  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;  
  
  -- Delete all lines
  Call DeleteLines();
  
  -- Open cursor
  OPEN CUR_TABLENAMES;  
  
  -- Create loop
  read_loop: LOOP
  
    -- Fetch the table name into the variable
    FETCH CUR_TABLENAMES INTO V_TableName;
    
    -- Exit if done
    IF done THEN
      LEAVE read_loop;
    End IF;
    
    -- Build log table statement
    Call InsertLine(GenerateLogTable(V_TableName, P_SchemaName));
  
  END LOOP;
  
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GenerateAllTriggers` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cs499`@`%` PROCEDURE `GenerateAllTriggers`(P_SchemaName varchar(64))
BEGIN
	  
  -- Declare return variable
  DECLARE V_Statement varchar(4000) DEFAULT NULL;
  
  -- Declare the done flag variable
  DECLARE done tinyint(1) DEFAULT FALSE;
  
  -- Declare table name variable
  DECLARE V_TableName varchar(64) DEFAULT NULL;
  
  -- Declare cursor and get the table names
  DECLARE CUR_TABLENAMES CURSOR FOR SELECT TABLE_NAME
                                    FROM information_schema.TABLES
                                    WHERE TABLE_SCHEMA = P_SchemaName
                                    AND TABLE_NAME NOT LIKE '%_log';
                                    
  -- Declare empty set flag
  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;  
  
  -- Delete all lines
  Call DeleteLines();
  
  -- Open cursor
  OPEN CUR_TABLENAMES;  
  
  -- Create loop
  read_loop: LOOP
  
    -- Fetch the table name into the variable
    FETCH CUR_TABLENAMES INTO V_TableName;
    
    -- Exit if done
    IF done THEN
      LEAVE read_loop;
    End IF;
    
    -- Build delete trigger statement
    Call InsertLine(GenerateBeforeDeleteTrigger(V_TableName, P_SchemaName));
    
    -- Build insert trigger statement
    Call InsertLine(GenerateBeforeInsertTrigger(V_TableName, P_SchemaName));
    
    -- Build update trigger statement
    Call InsertLine(GenerateBeforeUpdateTrigger(V_TableName, P_SchemaName));
    
  END LOOP;
  
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertLine` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`cs499`@`%` PROCEDURE `InsertLine`(P_Statement varchar(4000))
BEGIN
	
  INSERT INTO createdstatements(     
     Code
  ) VALUES (     
     P_Statement  -- Code - IN varchar(4000)
  );
  
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-04-14  2:15:29
