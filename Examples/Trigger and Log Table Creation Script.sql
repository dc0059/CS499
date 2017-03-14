/*
  Create the table to store the created statements
*/
CREATE TABLE `createdstatements` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT,
  `Code` varchar(4000) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

/*
  Create function to generate before delete triggers
*/
DROP FUNCTION IF EXISTS GenerateBeforeDeleteTrigger;
CREATE FUNCTION GenerateBeforeDeleteTrigger(P_TableName varchar(64), P_SchemaName varchar(64)) RETURNS varchar(4000) CHARSET utf8
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
  
END;

/*
  Create function to generate before insert triggers
*/
DROP FUNCTION IF EXISTS GenerateBeforeInsertTrigger;
CREATE FUNCTION GenerateBeforeInsertTrigger(P_TableName varchar(64), P_SchemaName varchar(64)) RETURNS varchar(4000) CHARSET utf8
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
  
END;

/*
  Create function to generate before update triggers
*/
DROP FUNCTION IF EXISTS GenerateBeforeUpdateTrigger;
CREATE FUNCTION GenerateBeforeUpdateTrigger(P_TableName varchar(64), P_SchemaName varchar(64)) RETURNS varchar(4000) CHARSET utf8
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
  
END;

/*
  Create function to generate log tables
*/
DROP FUNCTION IF EXISTS GenerateLogTable;
CREATE FUNCTION GenerateLogTable(P_TableName varchar(64), P_SchemaName varchar(64)) RETURNS varchar(4000) CHARSET utf8
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
  
END;

/*
  Create function to generate a comma delimited list of table column names
*/
DROP FUNCTION IF EXISTS GetCommaDelimitedTableColNames;
CREATE FUNCTION GetCommaDelimitedTableColNames(P_TableName varchar(64), P_SchemaName varchar(64), P_Prefix varchar(20)) RETURNS varchar(4000) CHARSET utf8
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
END;

/*
  Create function to generate a comma delimited list of table column names for the log table
*/
DROP FUNCTION IF EXISTS GetCommaDelimitedTableColNamesForLogTable;
CREATE FUNCTION GetCommaDelimitedTableColNamesForLogTable(P_TableName varchar(64), P_SchemaName varchar(64)) RETURNS varchar(4000) CHARSET utf8
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
END;

/*
  Create procedure to delete all created statements
*/
DROP PROCEDURE IF EXISTS DeleteLines;
CREATE PROCEDURE DeleteLines()
BEGIN
	
  DELETE FROM createdstatements;
  
END;

/*
  Create procedure to insert a created statement
*/
DROP PROCEDURE IF EXISTS InsertLine;
CREATE PROCEDURE InsertLine(P_Statement varchar(4000))
BEGIN
	
  INSERT INTO createdstatements(     
     Code
  ) VALUES (     
     P_Statement  -- Code - IN varchar(4000)
  );
  
END;

/*
  Create procedure to generate all log tables
*/
DROP PROCEDURE IF EXISTS GenerateAllLogTables;
CREATE PROCEDURE GenerateAllLogTables(P_SchemaName varchar(64))
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
  
END;

/*
  Create procedure to generate all triggers
*/
DROP PROCEDURE IF EXISTS GenerateAllTriggers;
CREATE PROCEDURE GenerateAllTriggers(P_SchemaName varchar(64))
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
  
END;







