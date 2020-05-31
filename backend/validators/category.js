const {check} = require('express-validator')

exports.catergoryCreateValidator = [
    check('name')
    .isLength({min: 1})
    .withMessage('Name is required')
];