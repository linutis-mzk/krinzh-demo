
CREATE SCHEMA test;
CREATE SCHEMA user_data;
CREATE SCHEMA dbo;

CREATE TABLE test.tblRows (
    row_id INT NOT NULL,
    column1 VARCHAR(250)
);

CREATE TABLE user_data.activities (
     id VARCHAR(80) NOT NULL,
     position SMALLINT,
     date TIMESTAMP,
     category VARCHAR(250),
     description VARCHAR(250),
     description_extra VARCHAR(800),
     important BOOLEAN,
     done BOOLEAN
);


CREATE TABLE dbo.parameters (
    pr_key VARCHAR(250) NOT NULL,
    pr_value VARCHAR(250)
);

CREATE TABLE dbo.streamers (
    user_id VARCHAR(80),
    user_name VARCHAR(80),
    display_name VARCHAR(80),
    custom_alias VARCHAR(80),
    profile_image_url VARCHAR(250)
);

CREATE TABLE dbo.streamers_status (
   time_updated TIMESTAMP,
   user_id VARCHAR(80),
   game_id VARCHAR(250),
   viewer_count INT,
   is_online BOOLEAN,
   stream_start_time TIMESTAMP,
   stream_end_time TIMESTAMP
);