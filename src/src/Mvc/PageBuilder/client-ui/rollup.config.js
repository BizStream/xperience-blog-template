import autoprefixer from 'autoprefixer'
import babel from '@rollup/plugin-babel'
import commonjs from '@rollup/plugin-commonjs'
import clean from 'rollup-plugin-cleaner'
import path from 'path'
import postcss from 'rollup-plugin-postcss'
import resolve from '@rollup/plugin-node-resolve'
import { terser } from 'rollup-plugin-terser'

const production = !process.env.ROLLUP_WATCH && process.env.MODE == 'release'

const configureBundle = (
  name,
  { dir, dest, src } = {
    dir: '../wwwroot/',
    dest: './',
    src: `${name}.js`
  }
) => {
  return {
    input: `src/${src}`,
    output: {
      dir: dir,
      entryFileNames: path.join(dest, `./${name}.min.js`).replace(/\\+/g, '/'),
      format: 'iife',
      plugins: [terser()],
      sourcemap: !production
    },
    plugins: [
      clean({ targets: [path.join(dir, dest, `./${name}.min.js`), path.join(dir, dest, `./${name}.min.css`)] }),
      resolve(),
      commonjs(),
      babel({
        babelrc: false,
        babelHelpers: 'runtime',
        exclude: [/\/core-js\//],
        plugins: ['@babel/plugin-transform-runtime'],
        presets: [
          [
            '@babel/preset-env',
            {
              corejs: 3,
              useBuiltIns: 'usage',
              targets: '> 0.25%, not dead, not ie 11, not op_mini all'
            }
          ]
        ]
      }),
      postcss({
        extract: path.join(dest, `./${name}.min.css`).replace(/\\+/g, '/'),
        inject: false,
        minimize: true,
        plugins: [autoprefixer],
        sourceMap: !production
      })
    ],
    watch: { exclude: 'node_modules/**' }
  }
}

const configureInlineEditor = (
  identifier,
  options = { src: `editors/${identifier}.js` }
) =>
    configureBundle(identifier, {
      dir: '../wwwroot',
      dest: `./PageBuilder/Admin/InlineEditors/${identifier}`,
      ...options
    })

  const configureSection = (
    identifier,
    options = { src: `sections/${identifier}.js` }
  ) =>
    configureBundle(identifier, {
      dir: '../wwwroot',
      dest: `./PageBuilder/Public/Sections/${identifier}`,
      ...options
    })

export default [
  configureSection('generic-column')
]