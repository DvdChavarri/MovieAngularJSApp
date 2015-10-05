/// <binding AfterBuild='uglify' />
module.exports = function (grunt) {
    // load Grunt plugins from NPM
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks("grunt-contrib-copy");
    grunt.loadNpmTasks('grunt-contrib-watch');

    // configure plugins
    grunt.initConfig({
        copy: {
            files: {
                cwd: '',
                src: ['Scripts/**/*.js', 'Scripts/**/*.css', 'Views/**/*.html'],
                dest: 'wwwroot',
                expand: true
            }
        },
        uglify: {
            options: {
                compress: {
                    drop_debugger: false
                }
            },
            my_target: {
                files: { 'wwwroot/app.js': ['Scripts/app.js', 'Scripts/**/*.js'] }
            }
        },

        watch: {
            scripts: {
                files: ['Scripts/**/*.js', 'Views/**/*.html'],
                tasks: ['copy']
            }
        }
    });

    // define tasks
    grunt.registerTask('default', ['copy', 'watch']);
};