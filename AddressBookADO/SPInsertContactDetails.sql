create procedure SPInsertContactDetails
(
@firstname varchar(50),
@lastname varchar(50),
@address varchar(100),
@city varchar(50),
@state varchar(50),
@zip int,
@phonenumber bigint,
@email varchar(50),
@dateadded varchar(30),
@addressbookname varchar(30)
)
as
Begin
     --set nocount on added to prevent extra result sets from
	 --interfering with select statements
Set nocount on;
begin try
Begin transaction
declare @firstnameexists varchar(50)
declare @lastnameexists varchar(50)
declare @zipexists int
declare @phonenumberexists bigint
declare @cityexists varchar(50)
declare @addressbooknameexists varchar(50)

		     --insert into address book table
			 select @firstnameexists=firstName ,@lastnameexists=lastname,@cityexists=city,@zipexists=zip,@phonenumberexists=phoneNumber
			 from addressbook
			 where firstname=@firstname and lastname=@lastname and address=@address and city=@city and state=@state and zip=@zip and phonenumber=@phonenumber
			 if(@firstnameexists is null and @lastnameexists is null and @zipexists is null)
				insert into addressbook(firstname,lastname,address,city,state,zip,phonenumber,email,dateAdded)
				values(@firstname,@lastname,@address,@city,@state,@zip,@phonenumber,@email,convert(date,@dateadded));
			else 
				begin
				Rollback Transaction
				end
			--insert into address book names
			select @addressbooknameexists=addressBookName from AddressBookNames where addressBookName=@addressbookname
			if(@addressbooknameexists is null)
				insert into AddressBookNames(addressBookName) values(@addressbookname);
			
		--if not error, commit transaction 
		commit transaction
		End Try
		Begin catch
		   --if error, roll back changes done by any of the sql queries
		  Rollback transaction
		End catch
End