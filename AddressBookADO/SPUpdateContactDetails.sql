create procedure SPUpdateContactDetails
(
@firstname varchar(30),
@lastname varchar(30),
@address varchar(50),
@city varchar(30),
@addressbookname varchar(30)
)
as
begin
	update a
	set a.address= @address,a.city=@city
	from AddressBook a
	join addressbookMapper b 
	on a.contactid= b.contactid
	join AddressBookNames c
	on c.addressBookId = b.addressbookid
	where a.firstName=@firstname and a.lastName=@lastname and c.addressBookName= @addressbookname
end