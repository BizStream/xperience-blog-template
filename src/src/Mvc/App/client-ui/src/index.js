import './styles/index.scss'

const onBeforeUnload = async e => {
  if (e.returnValue) return e.returnValue

  document.body.classList.add('unloading')
}

window.addEventListener('beforeunload', onBeforeUnload, false)
