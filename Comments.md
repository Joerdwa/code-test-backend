1. Mapping logic should be moved outside of the product application service into seperate mappers. This reduces duplication of mapping code and seperates the concern of mapping these objects to a seperate service.
2. Logic for submitting request is a bit messy. Better of using a simplified switch statement and moving each product submission into seperate private methods.
2. Structure of code needs shuffling. It's good practice to follow convetion of interfaces folder and namespacing.
3. Product file contains interface so should be correctly named IProduct.cs.
4. IProduct is a bit of an implicit interface in the fact it only defines an Id.
5. Would be good to make objects in solution immutable so they don't get manipulated once created.