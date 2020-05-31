const express = require('express');
const router = express.Router();
const {requiresSignin, authMiddleware} = require('../controllers/auth');
const {read} = require('../controllers/user');


router.get('/profile', requiresSignin, authMiddleware, read);

module.exports = router;
