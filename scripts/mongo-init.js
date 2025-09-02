// MongoDB initialization script
db = db.getSiblingDB('hypesoft');

// Create collections
db.createCollection('categories');
db.createCollection('products');

// Create indexes for better performance
db.categories.createIndex({ "name": 1 }, { unique: true });
db.products.createIndex({ "name": 1 });
db.products.createIndex({ "categoryId": 1 });
db.products.createIndex({ "stockQuantity": 1 });

// Insert sample categories
db.categories.insertMany([
  {
    _id: ObjectId(),
    name: "Electronics",
    description: "Electronic devices and accessories",
    createdAt: new Date(),
    updatedAt: new Date()
  },
  {
    _id: ObjectId(),
    name: "Clothing",
    description: "Fashion and apparel",
    createdAt: new Date(),
    updatedAt: new Date()
  },
  {
    _id: ObjectId(),
    name: "Books",
    description: "Books and educational materials",
    createdAt: new Date(),
    updatedAt: new Date()
  }
]);

// Insert sample products
const categories = db.categories.find().toArray();
const electronicsCategory = categories.find(c => c.name === "Electronics");
const clothingCategory = categories.find(c => c.name === "Clothing");
const booksCategory = categories.find(c => c.name === "Books");

db.products.insertMany([
  {
    _id: ObjectId(),
    name: "Smartphone",
    description: "Latest generation smartphone with advanced features",
    price: 999.99,
    categoryId: electronicsCategory._id,
    stockQuantity: 50,
    createdAt: new Date(),
    updatedAt: new Date()
  },
  {
    _id: ObjectId(),
    name: "Laptop",
    description: "High-performance laptop for professionals",
    price: 1299.99,
    categoryId: electronicsCategory._id,
    stockQuantity: 25,
    createdAt: new Date(),
    updatedAt: new Date()
  },
  {
    _id: ObjectId(),
    name: "T-Shirt",
    description: "Comfortable cotton t-shirt",
    price: 29.99,
    categoryId: clothingCategory._id,
    stockQuantity: 5,
    createdAt: new Date(),
    updatedAt: new Date()
  },
  {
    _id: ObjectId(),
    name: "Programming Book",
    description: "Complete guide to modern programming",
    price: 49.99,
    categoryId: booksCategory._id,
    stockQuantity: 15,
    createdAt: new Date(),
    updatedAt: new Date()
  }
]);

print("Database initialized successfully with sample data!");
