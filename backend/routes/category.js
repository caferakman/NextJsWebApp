const express = require('express')
const router = express.Router()
const { create } = require('../controllers/category');

//validators
const {runValidation} = require('../validators');
const { categoryCreateValidator } = require('../validators/category');
const { requiresSignin, adminMiddleware } = require('../controllers/auth');


router.post('/category', categoryCreateValidator, runValidation, requiresSignin, adminMiddleware, create);

module.exports = router;
