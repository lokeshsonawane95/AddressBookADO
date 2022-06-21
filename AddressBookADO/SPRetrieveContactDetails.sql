create Procedure SPRetrieveContactDetails
as
Begin
	select a.contactid,a.firstName,a.lastName,a.address,a.city,a.state,a.zip,a.phoneNumber,a.email,c.addressBookId,c.addressBookName,e.typeid,e.typename
	from addressbook a
	join addressbookMapper b
	on a.contactid= b.contactid
	join AddressBookNames c
	on b.addressbookid= c.addressBookId
	join typeMapper d
	on d.contactid= a.contactid
	join TypesOfContacts e
	on e.typeid= d.typeid
end